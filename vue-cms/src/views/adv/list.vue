<template>
  <div class="components-container">
    <split-pane :default-percent="15" split="vertical">
      <template slot="paneL">
        <el-menu default-active="1" class="el-menu-vertical-demo" @select="HandleMenuSelect">
          <el-menu-item v-for="item in types" :key="item.id" :index="item.flag"><i class="el-icon-menu" /><span slot="title">{{ item.title }}</span></el-menu-item>
        </el-menu>
      </template>
      <template slot="paneR">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <el-button v-for="item in btns.filter(item => item.Type==='button')" :type="item.Class" :id="item.DomId" :icon="'el-icon-' + item.Icon" :key="item.DomId" size="small" @click="handleBtnClick(item.DomId)">{{ item.Name }}</el-button>
          </div>
          <el-table v-loading="listLoading" :data="list" size="mini" border fit height="520" highlight-current-row style="width: 100%;min-height:500px;">
            <el-table-column v-for="col in listQuery.columns" :key="col.data" :label="col.name" :sortable="col.orderable">
              <template slot-scope="scope">{{ scope.row[col.data] }}</template>
            </el-table-column>
            <el-table-column align="center" label="操作" width="180" class-name="small-padding fixed-width">
              <template slot-scope="scope">
                <el-button v-for="item in btns.filter(item => item.Type==='inline')" :type="item.Class.replace('btn-','')" :id="item.DomId" :icon="'el-icon-' + item.Icon" :key="item.Id" size="mini" @click="handleBtnClick(item.DomId, scope.row)">{{ item.Name }}</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination :current-page="listQuery.draw" :page-sizes="[10,20,30, 50]" :page-size="listQuery.length" :total="total" background layout="total, sizes, prev, pager, next, jumper" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </el-card>
      </template>
    </split-pane>
    <!-- 编辑页 -->
    <el-dialog :visible.sync="dialogFormVisible" :title="editTitle">
      <el-form ref="formModel" :model="temp" :rules="rules" label-position="right" label-width="120px" style="width: 100%; margin: 0;">
        <el-row>
          <el-col :span="12"><el-form-item label="标题:" prop="title"><el-input v-model="temp.title" /></el-form-item></el-col>
          <el-col :span="12">
            <el-form-item label="类型:" prop="classId">
              <el-select v-model="temp.classId" placeholder="请选择">
                <el-option v-for="item in types" :key="item.id" :label="item.title" :value="item.id" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="链接方式:" prop="target">
              <el-select v-model="temp.target" placeholder="请选择">
                <el-option v-for="item in targets" :key="item.value" :label="item.label" :value="item.value" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12"><el-form-item label="权重:" prop="sort"><el-input-number v-model="temp.sort" :min="0" controls-position="right" /></el-form-item></el-col>
          <el-col :span="12"><el-form-item label="点击量:" prop="hits"><el-input-number v-model="temp.hits" :min="0" controls-position="right" /></el-form-item></el-col>
          <el-col :span="12">
            <el-form-item label="时间限制:" prop="isTimeLimit">
              <el-switch v-model="temp.isTimeLimit" active-text="启用" inactive-text="停用" />
            </el-form-item>
          </el-col>
          <el-col v-show="temp.isTimeLimit" :span="12"><el-form-item label="上线日期:" prop="beginTime"><el-date-picker v-model="temp.beginTime" type="datetime" placeholder="选择日期时间" /></el-form-item></el-col>
          <el-col v-show="temp.isTimeLimit" :span="12"><el-form-item label="下线日期:" prop="endTime"><el-date-picker v-model="temp.endTime" type="datetime" placeholder="选择日期时间" /></el-form-item></el-col>
        </el-row>
        <el-form-item label="图片地址:" prop="imgUrl">
          <el-upload :action="uploadurl" :show-file-list="false" :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload" class="avatar-uploader">
            <img v-if="temp.imgUrl" :src="temp.imgUrl" class="avatar">
            <i v-else class="el-icon-plus avatar-uploader-icon" />
          </el-upload>
        </el-form-item>
        <el-col :span="12">
          <el-form-item label="展示类型:" prop="types"><el-input-number v-model="temp.types" :min="1" controls-position="right" /></el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="状态:" prop="status">
            <el-switch v-model="temp.status" active-text="启用" inactive-text="停用" />
          </el-form-item>
        </el-col>
        <el-form-item label="链接地址:" prop="linkUrl"><el-input v-model="temp.linkUrl" /></el-form-item>
        <el-form-item label="文字描述:" prop="description"><el-input v-model="temp.description" type="textarea" /></el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button type="primary" @click="updateData">保存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import splitPane from 'vue-splitpane'
import { fetchList, getClassList, del, getAdv, create, update } from '@/api/adv'
import waves from '@/directive/waves'

export default {
  name: 'Adv',
  directives: { waves },
  components: { splitPane },
  data() {
    return {
      total: null,
      list: null,
      classId: null,
      types: [],
      targets: [{ value: '_blank', label: '新窗口' }, { value: '_self', label: '原窗口' }],
      listQuery: {
        draw: 1,
        start: 0,
        length: 10,
        columns: [
          { data: 'title', name: '标题', searchable: true, orderable: true },
          { data: 'classId', name: '类型', searchable: true, orderable: true, search: { regex: false, opeartor: 'Equal' }},
          { data: 'linkUrl', name: '链接地址', searchable: true, orderable: true },
          { data: 'status', name: '状态', searchable: true, orderable: true },
          { data: 'sort', name: '权重', searchable: true, orderable: true },
          { data: 'lastUpdateDate', name: '更新日期', searchable: true, orderable: true }
        ]
      },
      listLoading: true,
      btns: [
        { DomId: 'btnAdd', Name: '新增', Type: 'button', Class: 'primary', Icon: 'plus' },
        { DomId: 'btnEdit', Name: '编辑', Type: 'inline', Class: 'success', Icon: 'edit' },
        { DomId: 'btnDel', Name: '删除', Type: 'inline', Class: 'danger', Icon: 'delete' }
      ],
      dialogFormVisible: false,
      editTitle: '',
      temp: {},
      rules: {
        title: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        classId: [{ required: true, message: '请选择类型', trigger: 'change' }]
      },
      uploadurl: process.env.BASE_API + '/CMSAPI/Media/Upload'
    }
  },
  created() {
    getClassList().then(r => {
      this.types = r.data
      if (this.types) {
        this.classId = this.types[0].id
        this.handleFilter()
      }
    })
  },
  methods: {
    getList() {
      this.listLoading = true
      this.list = []
      this.listQuery.columns[1].search.value = this.classId
      fetchList(this.listQuery).then(response => {
        this.listLoading = false
        let list = response.data.data
        if (list && list.length > 0) {
          list = list.map(item => {
            const type = this.types.filter(o => { return o.id === item.classId })
            if (type && type.length > 0) {
              item.classId = type[0].title
            }
            item.status = item.status ? '启用' : '停用'
            return item
          })
          this.list = list
        }
        this.total = response.data.recordsTotal
      }).catch(() => {
        this.listLoading = false
      })
    },
    handleFilter() {
      this.listQuery.draw = 1
      this.listQuery.start = 0
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.length = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.draw = val
      this.listQuery.start = (val - 1) * this.listQuery.length
      this.getList()
    },
    handleBtnClick(domid, row) {
      switch (domid) {
        case 'btnAdd':
          this.editTitle = '新增'
          this.temp = { classId: this.classId, types: 1, isTimeLimit: false, hits: 0, sort: 0 }
          this.dialogFormVisible = true
          this.$nextTick(() => {
            this.$refs['formModel'].clearValidate()
          })
          break
        case 'btnEdit':
          this.editTitle = '编辑'
          this.handleUpdate(row)
          break
        case 'btnDel':
          this.handleDelete(row)
          break
      }
    },
    handleUpdate(row) {
      getAdv(row.id).then(response => {
        this.temp = response.data
        this.temp.status = this.temp.status > 0
        this.dialogFormVisible = true
        this.$nextTick(() => {
          this.$refs['formModel'].clearValidate()
        })
      }).catch(err => {
        console.log(err)
      })
    },
    handleDelete(row) {
      this.$confirm('确定要删除当前数据?', '提示', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }).then(() => {
        this.listLoading = true
        del(row.id).then(response => {
          this.listLoading = false
          this.$notify({ title: '成功', message: '删除成功', type: 'success', duration: 2000 })
          const index = this.list.indexOf(row)
          this.list.splice(index, 1)
        }).catch(() => {
          this.listLoading = false
        })
      })
    },
    updateData() {
      this.$refs['formModel'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          tempData.id = tempData.id || 0
          tempData.actionType = tempData.id !== 0 ? 2 : 1
          tempData.status = tempData.status ? 1 : 0
          const opt = tempData.id !== 0 ? update(tempData) : create(tempData)
          opt.then(response => {
            this.dialogFormVisible = false
            this.handleFilter()
            this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
          }).catch(() => { this.dialogFormVisible = false })
        }
      })
    },
    HandleMenuSelect(index, indexPath) {
      this.classId = Number(index)
      this.handleFilter()
    },
    handleAvatarSuccess(res, file) {
      this.temp.imgUrl = process.env.BASE_API + res.url
    },
    beforeAvatarUpload(file) {
      const isLt2M = file.size / 1024 / 1024 < 2
      if (!isLt2M) {
        this.$message.error('上传图片大小不能超过 2MB!')
      }
      return isLt2M
    }
  }
}
</script>

<style>
  .avatar-uploader .el-upload {
    border: 1px dashed #d9d9d9;
    border-radius: 6px;

    cursor: pointer;
    position: relative;
    overflow: hidden;
  }
  .avatar-uploader .el-upload:hover {
    border-color: #409EFF;
  }
  .avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    line-height: 178px;
    text-align: center;
  }
  .avatar {
    width: 178px;
    height: 178px;
    display: block;
  }
</style>
