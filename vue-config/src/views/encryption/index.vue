<template>
  <div class="app-container">
    <el-row>
      <el-col :span="12" :offset="5">
        <el-card :body-style="{ padding: '30px' }">
          <el-form ref="formModel" :model="temp" :rules="rules" label-position="right" label-width="120px" style="margin-left:10px;margin-right:10px;">
            <el-form-item label="文件名称:" prop="fileName"><el-input v-model="temp.fileName" :disabled="!canUpload" name="fileName" /></el-form-item>
            <el-form-item label="上传文件:" prop="fileList">
              <el-upload ref="upload" :action="url" :data="temp" :file-list="temp.fileList" :auto-upload="false" :on-success="onSuccess">
                <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
                <div slot="tip" class="el-upload__tip">文件不超过1Mb</div>
              </el-upload>
            </el-form-item>
          </el-form>
          <div style="padding: 14px;">
            <div class="bottom clearfix">
              <el-button v-if="canUpload" type="primary" class="button" @click="submitUpload">上传加密</el-button>
              <template v-else>
                <el-link type="success" icon="el-icon-download" :href="download + '?name=' + temp.fileName">下载加密文件</el-link>
                <el-link type="warning" icon="el-icon-refresh-right" style="margin-left:30px;" @click="onReload">重新上传</el-link>
              </template>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>

export default {
  data() {
    return {
      url: process.env.VUE_APP_BASE_API + '/Encryption/UploadConfig',
      download: process.env.VUE_APP_BASE_API + '/Encryption/GetFile',
      temp: { fileName: '', fileList: [] },
      rules: {
        fileName: [{ required: true, message: '文件名称', trigger: 'blur' }]
      },
      canUpload: true
    }
  },
  methods: {
    onSuccess() {
      this.canUpload = false
      this.$notify({ title: '成功', message: '已加密，请点击下载', type: 'success', duration: 2000 })
    },
    submitUpload() {
      this.$refs['formModel'].validate((valid) => {
        if (valid) {
          this.$refs.upload.submit()
        }
      })
    },
    onReload() {
      location.reload()
    }
  }

}
</script>
