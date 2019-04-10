<template>
  <div class="components-container">
    <split-pane :default-percent="15" split="vertical">
      <template slot="paneL">
        <div class="left-container">
          <div class="tree-nav-title">路由分类</div>
          <el-tree :data="casOrgs" :props="defaultProps" :expand-on-click-node="false" default-expand-all highlight-current class="left-tree" @node-click="handleNodeClick">
            <span slot-scope="{ node, data }" class="custom-tree-node">
              <span> <svg-icon :icon-class="data.children && data.children.length > 0 ? 'folder':'doc'" class="el-tree-icon" />{{ node.label }}</span>
            </span>
          </el-tree>
        </div>
      </template>
      <template slot="paneR">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <el-form ref="queryForm" :inline="true" :model="listQuery">
              <el-form-item label="机构名称:" prop="name">
                <el-input v-model="listQuery.name" size="small" class="filter-item" placeholder="机构名称" />
              </el-form-item>
              <el-form-item>
                <el-button v-waves type="primary" class="filter-item" icon="el-icon-search" size="small" @click="handleFilter">查询</el-button>
                <el-button v-waves class="filter-item" icon="el-icon-refresh" size="small" @click="resetForm('queryForm')">重置</el-button>
                <el-button v-for="item in btns.filter(item => item.Type==='button')" :id="item.DomId" :key="item.DomId" :type="item.Class" :icon="'el-icon-' + item.Icon" size="small" @click="handleBtnClick(item.DomId)">{{ item.Name }}</el-button>
              </el-form-item>
            </el-form>
          </div>
          <el-table v-loading="listLoading" :data="list" size="mini" border fit height="520" highlight-current-row style="width: 100%;min-height:500px;">
            <el-table-column v-for="col in columns" :key="col.data" :label="col.name" :sortable="col.orderable">
              <template slot-scope="scope">{{ scope.row[col.data] }}</template>
            </el-table-column>
            <el-table-column align="center" label="状态" width="100">
              <template slot-scope="scope">
                <el-tag :type="scope.row.infoStatus ? 'success' : 'danger'">{{ scope.row.infoStatus ? '有效' : '无效' }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column align="center" label="操作" width="180" class-name="small-padding fixed-width">
              <template slot-scope="scope">
                <el-button v-for="item in btns.filter(item => item.Type==='inline')" :id="item.DomId" :key="item.Id" :type="item.Class.replace('btn-','')" :icon="'el-icon-' + item.Icon" size="mini" @click="handleBtnClick(item.DomId, scope.row)">{{ item.Name }}</el-button>
              </template>
            </el-table-column>
          </el-table>
          <el-pagination :current-page="listQuery.draw" :page-sizes="[10,20,30, 50]" :page-size="listQuery.pageSize" :total="total" background layout="total, sizes, prev, pager, next, jumper" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
        </el-card>
      </template>
    </split-pane>
    <!-- 编辑页 -->
    <el-dialog :visible.sync="dialogFormVisible" :title="editTitle">
      <el-form ref="formModel" :model="temp" :rules="rules" label-position="right" label-width="120px" style="width: 400px; margin-left:50px;">
        <el-form-item label="名称:" prop="name"><el-input v-model="temp.name" /></el-form-item>
        <el-form-item label="上级组织:" prop="parentId">
          <el-cascader v-model="selectedOptions" :options="casOrgs" change-on-select placeholder="请选择" style="width:100%" @change="handleCascaderChange" />
        </el-form-item>
        <el-form-item label="业务编码:" prop="bizCode"><el-input v-model="temp.bizCode" /></el-form-item>
        <el-form-item label="状态:" prop="status">
          <el-switch v-model="temp.status" active-text="启用" inactive-text="停用" />
        </el-form-item>
        <el-form-item label="序号:" prop="sort"><el-input v-model="temp.sort" /></el-form-item>
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
import { getPageList, getCascaderList, edit, del } from '@/api/reroute'
import waves from '@/directive/waves'

export default {
  name: 'Org',
  directives: { waves },
  components: { splitPane },
  data() {
    return {
      total: 0,
      list: [],
      listLoading: false,
      listQuery: {
        draw: 0,
        page: 0,
        pageSize: 10,
        deptId: null,
        name: ''
      },
      columns: [
        { data: 'upstreamPathTemplate', name: '上游路径', searchable: true, orderable: true },
        { data: 'upstreamHttpMethod', name: '上游请求方法', searchable: true, orderable: true },
        { data: 'downstreamScheme', name: '下游请求架构', searchable: true, orderable: true },
        { data: 'downstreamPathTemplate', name: '下游路径', searchable: true, orderable: true },
        { data: 'downstreamHostAndPorts', name: '下游地址', searchable: true, orderable: true },
        { data: 'authenticationOptions', name: '授权配置', searchable: true, orderable: true }
      ],
      btns: [
        { DomId: 'btnAdd', Name: '新增', Type: 'button', Class: 'primary', Icon: 'plus' },
        { DomId: 'btnEdit', Name: '编辑', Type: 'inline', Class: 'success', Icon: 'edit' },
        { DomId: 'btnDel', Name: '删除', Type: 'inline', Class: 'danger', Icon: 'delete' }
      ],
      defaultProps: {
        children: 'children',
        label: 'label'
      },
      dialogFormVisible: false,
      temp: {
        keyId: null,
        name: '',
        bizCode: '',
        status: true,
        sort: 0,
        parentId: null
      },
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        bizCode: [{ required: true, message: '请输入业务编码', trigger: 'blur' }],
        parentId: [{ required: true, message: '请选择上级组织', trigger: 'change' }]
      },
      editTitle: '',
      casOrgs: [],
      selectedOptions: []
    }
  },
  created() {
    this.handleFilter()
    getCascaderList().then(r => { this.casOrgs = r.result })
  },
  methods: {
    getList() {
      this.listLoading = true
      this.list = []
      getPageList(this.listQuery).then(response => {
        this.listLoading = false
        this.list = response.result.rows
        this.total = response.result.total
      }).catch(() => {
        this.listLoading = false
      })
    },
    handleFilter() {
      this.listQuery.page = 0
      this.getList()
    },
    handleSizeChange(val) {
      this.listQuery.pageSize = val
      this.getList()
    },
    handleCurrentChange(val) {
      this.listQuery.page = val
      this.getList()
    },
    resetForm(formName) {
      this.$refs[formName].resetFields()
    },
    handleNodeClick(data) {
      this.listQuery.deptId = data.value
      this.getList()
    },
    handleCascaderChange(val) {
      if (!val || val.length < 1) return
      this.temp.parentId = val[val.length - 1]
    },
    handleBtnClick(domid, row) {
      switch (domid) {
        case 'btnAdd':
          this.editTitle = '新增'
          this.selectedOptions = [this.listQuery.deptId]
          this.temp = { parentId: this.listQuery.deptId }
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
      this.temp = Object.assign({}, row)
      this.selectedOptions = []
      try {
        this.casOrgs.filter(item => {
          this.getNodePath(item)
        })
      } catch (e) {
        console.log(e)
      }
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['formModel'].clearValidate()
      })
    },
    getNodePath(node) {
      this.selectedOptions.push(node.value)
      if (node.value === this.temp.parentId) { // 找到符合条件的节点，终止掉递归
        throw new Error('end node path')
      }
      if (node.children && node.children.length > 0) {
        for (var i = 0; i < node.children.length; i++) {
          this.getNodePath(node.children[i])
        }
        this.selectedOptions.pop() // 当前节点的子节点遍历完依旧没找到，则删除路径中的该节点
      } else {
        this.selectedOptions.pop() // 找到叶子节点时，删除路径当中的该叶子节点
      }
    },
    handleDelete(row) {
      this.$confirm('删除操作不可逆，请问是否删除?', '提示', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }).then(() => {
        this.listLoading = true
        del(row.keyId).then(response => {
          this.listLoading = false
          if (response && response.data.success) {
            this.$notify({ title: '成功', message: '删除成功', type: 'success', duration: 2000 })
            this.refreshView()
          } else {
            this.$message({ type: 'error', message: response.Message || '删除失败' })
          }
        }).catch(() => { this.listLoading = false })
      })
    },
    updateData() {
      this.$refs['formModel'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          tempData.keyId = tempData.keyId || '00000000-0000-0000-0000-000000000000'
          edit(tempData).then(response => {
            this.dialogFormVisible = false
            if (response && response.data.success) {
              this.refreshView()
              this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
            } else {
              this.$message({ type: 'error', message: response.data.msg || '更新失败' })
            }
          }).catch(() => { this.dialogFormVisible = false })
        }
      })
    },
    refreshView() {
      this.$store.dispatch('delAllCachedViews', this.$route)
      const { fullPath } = this.$route
      this.$nextTick(() => {
        this.$router.replace({
          path: '/redirect' + fullPath
        })
      })
    }
  }

}
</script>

<style  scoped>
  .components-container {
    position: relative;
    margin: 0;
    height: calc(100vh - 200px);
  }
  .left-container{
    width:100%;
    height:100%;
    overflow-y: auto;
    background-color: #eaedf1;
  }
  .left-tree{
    width: 100%;
    padding: 10px 5px;
  }
  .tree-nav-title{
    width: 100%;
    height: 70px;
    line-height: 70px;
    background: #d9dee4;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    font-weight: bold;
    text-indent: 20px;
  }
</style>
