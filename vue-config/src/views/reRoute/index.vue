<template>
  <div class="components-container">
    <split-pane :default-percent="15" split="vertical">
      <template slot="paneL">
        <div class="left-container">
          <div class="tree-nav-title">路由分类</div>
          <div style="float:right;margin-top:-50px;padding: 0 5px">
            <el-tooltip class="item" effect="dark" content="添加分类" placement="top-start">
              <el-button v-waves round type="success" icon="el-icon-plus" size="mini" style="padding: 7px 8px;" @click="handleBtnClick('btnAddItem')" />
            </el-tooltip>
            <el-tooltip class="item" effect="dark" content="删除分类" placement="top-start">
              <el-button v-waves round type="danger" icon="el-icon-delete" size="mini" style="padding: 7px 8px;" @click="handleItemDel" />
            </el-tooltip>
          </div>
          <el-tree :data="cases" :props="defaultProps" :expand-on-click-node="false" default-expand-all highlight-current class="left-tree" @node-click="handleNodeClick">
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
              <el-form-item label="上游路径:" prop="name">
                <el-input v-model="listQuery.name" size="small" class="filter-item" placeholder="上游路径" />
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
        <el-form-item label="路由分类:" prop="itemId">
          <el-cascader v-model="selectedOptions" :options="cases" change-on-select placeholder="请选择" style="width:100%" @change="handleCascaderChange" />
        </el-form-item>
        <el-form-item label="请求Key:" prop="requestIdKey"><el-input v-model="temp.requestIdKey" /></el-form-item>
        <el-form-item label="上游域名:" prop="upstreamHost"><el-input v-model="temp.upstreamHost" /></el-form-item>
        <el-form-item label="上游路径:" prop="upstreamPathTemplate"><el-input v-model="temp.upstreamPathTemplate" /></el-form-item>
        <el-form-item label="上游请求方法:" prop="upstreamHttpMethod"><el-input v-model="temp.upstreamHttpMethod" /></el-form-item>
        <el-form-item label="下游请求架构:" prop="downstreamScheme"><el-input v-model="temp.downstreamScheme" /></el-form-item>
        <el-form-item label="下游路径:" prop="downstreamPathTemplate"><el-input v-model="temp.downstreamPathTemplate" /></el-form-item>
        <el-form-item label="下游地址:" prop="downstreamHostAndPorts"><el-input v-model="temp.downstreamHostAndPorts" /></el-form-item>
        <el-form-item label="授权配置:" prop="authenticationOptions"><el-input v-model="temp.authenticationOptions" /></el-form-item>
        <el-form-item label="缓存配置:" prop="cacheOptions"><el-input v-model="temp.cacheOptions" /></el-form-item>
        <el-form-item label="服务发现名称:" prop="serviceName"><el-input v-model="temp.serviceName" /></el-form-item>
        <el-form-item label="负载均衡配置:" prop="loadBalancerOptions"><el-input v-model="temp.loadBalancerOptions" /></el-form-item>
        <el-form-item label="请求安全配置:" prop="qoSOptions"><el-input v-model="temp.qoSOptions" /></el-form-item>
        <el-form-item label="路由优先级:" prop="priority"><el-input v-model="temp.priority" /></el-form-item>
        <el-form-item label="状态:" prop="infoStatus">
          <el-switch v-model="temp.infoStatus" :active-value="1" active-text="有效" inactive-text="无效" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取消</el-button>
        <el-button type="primary" @click="updateData">保存</el-button>
      </div>
    </el-dialog>
    <!-- 编辑页 -->
    <el-dialog :visible.sync="dialogItemFormVisible">
      <el-form ref="itemModel" :model="itemModel" label-position="right" label-width="120px" style="width: 400px; margin-left:50px;">
        <el-form-item label="上级分类:" prop="ItemParentId">
          <el-cascader v-model="selectedOptions" :options="cases" change-on-select placeholder="请选择" style="width:100%" @change="handleItemCascaderChange" />
        </el-form-item>
        <el-form-item label="分类名称:" prop="itemName"><el-input v-model="itemModel.itemName" /></el-form-item>
        <el-form-item label="分类描述:" prop="itemDetail"><el-input v-model="itemModel.itemDetail" type="textarea" /></el-form-item>
        <el-form-item label="状态:" prop="infoStatus">
          <el-switch v-model="itemModel.infoStatus" :active-value="1" active-text="有效" inactive-text="无效" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogItemFormVisible = false">取消</el-button>
        <el-button type="primary" @click="updateItem">保存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import splitPane from 'vue-splitpane'
import { getPageList, getCascaderList, edit, del, editType, delType } from '@/api/reroute'
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
        itemId: null,
        name: ''
      },
      columns: [
        { data: 'requestIdKey', name: '请求Key', searchable: true, orderable: true },
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
      defaultProps: { children: 'children', label: 'label' },
      dialogFormVisible: false,
      temp: {},
      rules: {
        upstreamPathTemplate: [{ required: true, message: '上游路径', trigger: 'blur' }],
        downstreamPathTemplate: [{ required: true, message: '下游路径', trigger: 'blur' }],
        upstreamHttpMethod: [{ required: true, message: '上游请求方法', trigger: 'blur' }],
        downstreamScheme: [{ required: true, message: '下游请求架构', trigger: 'blur' }],
        downstreamHostAndPorts: [{ required: true, message: '下游地址', trigger: 'blur' }],
        itemId: [{ required: true, message: '请选择上级组织', trigger: 'change' }]
      },
      editTitle: '',
      cases: [],
      selectedOptions: [],
      itemModel: {},
      dialogItemFormVisible: false
    }
  },
  created() {
    this.handleFilter()
    getCascaderList().then(r => { this.cases = r.result })
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
      this.listQuery.itemId = data.value
      this.getList()
    },
    handleCascaderChange(val) {
      if (!val || val.length < 1) return
      this.temp.itemId = val[val.length - 1]
    },
    handleItemCascaderChange(val) {
      if (!val || val.length < 1) return
      this.itemModel.itemParentId = val[val.length - 1]
    },
    handleBtnClick(domid, row) {
      switch (domid) {
        case 'btnAdd':
          this.editTitle = '新增'
          this.selectedOptions = [this.listQuery.itemId]
          this.temp = { itemId: this.listQuery.itemId, infoStatus: 1 }
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
        case 'btnAddItem':
          this.selectedOptions = [this.listQuery.itemId]
          this.itemModel = { itemParentId: this.listQuery.itemId, infoStatus: 1 }
          this.dialogItemFormVisible = true
          this.$nextTick(() => {
            this.$refs['itemModel'].clearValidate()
          })
          break
      }
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row)
      this.selectedOptions = []
      try {
        this.cases.filter(item => {
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
      if (node.value === this.temp.itemId) { // 找到符合条件的节点，终止掉递归
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
        del(row.reRouteId).then(response => {
          this.listLoading = false
          if (response && response.success) {
            this.handleFilter()
            this.$notify({ title: '成功', message: '删除成功', type: 'success', duration: 2000 })
          } else {
            this.$message({ type: 'error', message: response.msg || '删除失败' })
          }
        }).catch(() => { this.listLoading = false })
      })
    },
    handleItemDel() {
      if (!this.listQuery.itemId) {
        this.$message({ type: 'error', message: '请选择要删除的分类' })
      }
      this.$confirm('删除操作不可逆，请问是否删除?', '提示', { confirmButtonText: '确定', cancelButtonText: '取消', type: 'warning' }).then(() => {
        delType(this.listQuery.itemId).then(response => {
          if (response && response.success) {
            getCascaderList().then(r => { this.cases = r.result })
            this.$notify({ title: '成功', message: '删除成功', type: 'success', duration: 2000 })
          } else {
            this.$message({ type: 'error', message: response.msg || '删除失败' })
          }
        })
      })
    },
    updateData() {
      this.$refs['formModel'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          edit(tempData).then(response => {
            this.dialogFormVisible = false
            if (response && response.success) {
              this.handleFilter()
              this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
            } else {
              this.$message({ type: 'error', message: response.msg || '更新失败' })
            }
          }).catch(() => { this.dialogFormVisible = false })
        }
      })
    },
    updateItem() {
      this.$refs['itemModel'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.itemModel)
          editType(tempData).then(response => {
            this.dialogItemFormVisible = false
            if (response && response.success) {
              getCascaderList().then(r => { this.cases = r.result })
              this.$notify({ title: '成功', message: '更新成功', type: 'success', duration: 2000 })
            } else {
              this.$message({ type: 'error', message: response.msg || '更新失败' })
            }
          })
        }
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
