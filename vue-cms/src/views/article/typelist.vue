<template>
  <el-row :gutter="20">
    <el-col :span="16" :offset="4">
      <el-card class="box-card" shadow="hover" style="margin-top:30px;">
        <div slot="header" class="clearfix">
          <span>文章类别</span>
          <el-button style="float: right; padding: 3px 0" type="text" @click="handleAddClick">添加类别</el-button>
          <el-button style="float: right; padding: 3px 10px 3px 0" type="text" @click="getTrees">刷新</el-button>
        </div>
        <el-tree v-loading="loading" :data="navlist" node-key="id" default-expand-all>
          <span slot-scope="{ node, data }" class="custom-tree-node">
            <span>{{ node.label }}</span>
            <span>
              <el-button type="text" size="mini" @click="() => append(data)">添加下级类别</el-button>
              <el-button type="text" size="mini" @click="() => edit(data)">编辑</el-button>
              <el-button type="text" size="mini" @click="() => remove(node, data)">删除</el-button>
            </span>
          </span>
        </el-tree>
      </el-card>
    </el-col>
    <!-- 编辑页 -->
    <el-dialog :visible.sync="dialogFormVisible" :title="editTitle">
      <el-form ref="formModel" :model="temp" :rules="rules" label-position="left" label-width="100px" style="width: 400px; margin-left:50px;">
        <el-form-item label="标题" prop="title"><el-input v-model="temp.title" /></el-form-item>
        <el-form-item label="Url" prop="url"><el-input v-model="temp.url" /></el-form-item>
        <el-form-item label="上级目录:" prop="parentName"><el-input v-model="temp.parentName" :disabled="true" /></el-form-item>
        <el-form-item label="SEO标题" prop="sEOTitle"><el-input v-model="temp.sEOTitle" /></el-form-item>
        <el-form-item label="SEO关键字" prop="sEOKeyWord"><el-input v-model="temp.sEOKeyWord" /></el-form-item>
        <el-form-item label="SEO描述" prop="sEODescription"><el-input v-model="temp.sEODescription" /></el-form-item>
        <el-form-item label="是否有效" prop="status">
          <el-select v-model="temp.status" class="filter-item" placeholder="请选择">
            <el-option v-for="item in opts" :key="item.Id" :label="item.Title" :value="item.Id" />
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button type="primary" @click="updateData">保存</el-button>
      </div>
    </el-dialog>
  </el-row>
</template>

<script>
import { getArticleTypeTree, delType, createType, editType, getTypeById } from '@/api/article'

export default{
  data() {
    return {
      navlist: [],
      loading: false,
      dialogFormVisible: false,
      editTitle: '',
      opts: [{ Id: 1, Title: '有效' }, { Id: 0, Title: '无效' }],
      temp: {},
      rules: {
        title: [{ required: true, message: '请输入标题', trigger: 'blur' }]
      }
    }
  },
  created() {
    this.getTrees()
  },
  methods: {
    append(data) {
      this.temp = {
        parentName: data.label,
        parentId: data.id,
        status: 1
      }
      this.editTitle = '新增'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['formModel'].clearValidate()
      })
    },
    edit(data) {
      this.loading = true
      getTypeById(data.id).then(response => {
        this.loading = false
        this.temp = response.data
        this.editTitle = '编辑'
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['formModel'].clearValidate()
        })
      }).catch(() => {
        this.loading = false
      })
    },
    remove(node, data) {
      this.$confirm('删除类别会删除对应的子类别，确定要删除吗？', '提示', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }).then(() => {
        const parent = node.parent
        const children = parent.data.children || parent.data
        this.loading = true
        delType(node.data.id).then(response => {
          this.loading = false
          const index = children.findIndex(d => d.id === data.id)
          children.splice(index, 1)
        }).catch(() => {
          this.loading = false
        })
      })
    },
    handleAddClick() {
      this.temp = {
        parentName: '根节点',
        status: 1
      }
      this.editTitle = '新增'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['formModel'].clearValidate()
      })
    },
    getTrees() {
      this.loading = true
      getArticleTypeTree().then(response => {
        this.loading = false
        this.navlist = response.data
      }).catch(() => {
        this.loading = false
      })
    },
    updateData() {
      this.$refs['formModel'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          let opt
          if (tempData.id && tempData.id !== '00000000-0000-0000-0000-000000000000') {
            opt = editType(tempData)
          } else {
            opt = createType(tempData)
          }
          opt.then(response => {
            this.dialogFormVisible = false
            this.getTrees()
            this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
          }).catch((error) => {
            this.dialogFormVisible = false
            this.$message({ type: 'error', message: error || '更新失败' })
          })
        }
      })
    }
  }
}
</script>

<style>
  .custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
  }
</style>
