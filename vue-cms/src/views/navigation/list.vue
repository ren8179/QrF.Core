<template>
  <el-row :gutter="20">
    <el-col :span="16" :offset="4">
      <el-card class="box-card" shadow="hover" style="margin-top:30px;">
        <div slot="header" class="clearfix">
          <span>导航</span>
          <el-button style="float: right; padding: 3px 0" type="text" @click="handleAddClick">添加导航</el-button>
        </div>
        <el-tree v-loading="loading" :data="navlist" node-key="id" default-expand-all draggable @node-drag-end="handleDragEnd" @node-drop="handleDrop">
          <span slot-scope="{ node, data }" class="custom-tree-node">
            <span>{{ node.label }}</span>
            <span>
              <el-button type="text" size="mini" @click="() => append(data)">添加下级导航</el-button>
              <el-button type="text" size="mini" @click="() => remove(node, data)">删除</el-button>
            </span>
          </span>
        </el-tree>
      </el-card>
    </el-col>
    <!-- 编辑页 -->
    <el-dialog :visible.sync="dialogFormVisible" :title="editTitle">
      <el-form ref="formModel" :model="temp" :rules="rules" label-position="left" label-width="100px" style="width: 400px; margin-left:50px;">
        <el-form-item label="标题" prop="Title"><el-input v-model="temp.Title" /></el-form-item>
        <el-form-item label="Url" prop="Url"><el-input v-model="temp.Url" /></el-form-item>
        <el-form-item label="上级目录:" prop="ParentName"><el-input v-model="temp.ParentName" :disabled="true" /></el-form-item>
        <el-form-item label="HTML内容" prop="Html"><el-input v-model="temp.Html" type="textarea" /></el-form-item>
        <el-form-item label="是否有效" prop="Status">
          <el-select v-model="temp.Status" class="filter-item" placeholder="请选择">
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
import { getNavTree, delNav, createNav, editNav } from '@/api/navigation'

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
        Title: [{ required: true, message: '请输入标题', trigger: 'blur' }],
        Url: [{ required: true, message: '请输入Url', trigger: 'blur' }]
      }
    }
  },
  created() {
    this.getNavTree()
  },
  methods: {
    append(data) {
      this.temp = {
        ParentName: data.label,
        ParentId: data.id,
        Status: 1
      }
      this.editTitle = '新增'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['formModel'].clearValidate()
      })
    },
    remove(node, data) {
      this.$confirm('删除导航会删除对应的子导航，确定要删除吗？', '提示', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }).then(() => {
        const parent = node.parent
        const children = parent.data.children || parent.data
        this.loading = true
        delNav(node.data.id).then(response => {
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
        ParentName: '根节点',
        Status: 1
      }
      this.editTitle = '新增'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['formModel'].clearValidate()
      })
    },
    handleDragEnd(draggingNode, dropNode, dropType, ev) {
      console.log('tree drag end: ', dropNode && dropNode.label, dropType)
    },
    handleDrop(draggingNode, dropNode, dropType, ev) {
      console.log('tree drop: ', dropNode.label, dropType)
    },
    getNavTree() {
      this.loading = true
      getNavTree().then(response => {
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
          let opt = createNav(tempData)
          if (tempData.Id || tempData.Id === '00000000-0000-0000-0000-000000000000') {
            opt = editNav(tempData)
          }
          opt.then(response => {
            this.dialogFormVisible = false
            if (response && response.Status) {
              this.getNavTree()
              this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
            } else {
              this.$message({ type: 'error', message: response.Message || '更新失败' })
            }
          }).catch(() => {
            this.dialogFormVisible = false
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
