<template>
  <div class="ass-container">
    <split-pane :default-percent="20" split="vertical">
      <template slot="paneL">
        <div class="left-container">
          <el-tree ref="tree" :data="trees" :props="defaultProps" :expand-on-click-node="false" default-expand-all highlight-current node-key="value" class="left-tree" @node-click="handleNodeClick">
            <span slot-scope="{ node, data }" class="custom-tree-node">
              <span> <svg-icon :icon-class="data.children && data.children.length > 0 ? 'folder':'doc'" class="el-tree-icon" />{{ node.label }}</span>
            </span>
          </el-tree>
        </div>
      </template>
      <template slot="paneR">
        <el-table v-loading="listLoading" :data="list" size="mini" border fit highlight-current-row style="width: 100%;min-height:255px;">
          <el-table-column v-for="col in columns" :key="col.data" :label="col.name" :sortable="col.orderable">
            <template slot-scope="scope">{{ scope.row[col.data] }}</template>
          </el-table-column>
          <el-table-column align="center" label="授权" width="160" class-name="small-padding fixed-width">
            <template slot-scope="scope">
              <el-tooltip :content="scope.row.isAuth?'已授权':'未授权'" placement="top">
                <el-switch v-model="scope.row.isAuth" active-text="已授权" inactive-text="未授权" @change="handleAccessChange(scope.row.isAuth, scope.row)" />
              </el-tooltip>
            </template>
          </el-table-column>
        </el-table>
        <el-pagination :current-page="listQuery.draw" :page-sizes="[5,10,20,50]" :page-size="listQuery.pageSize" :total="total" background layout="total, sizes, prev, pager, next, jumper" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
      </template>
    </split-pane>
  </div>
</template>

<script>
import splitPane from 'vue-splitpane'
import { getCascaderList, getAccGroupList } from '@/api/reroute'
import { toAccReRoute } from '@/api/authgroup'

export default {
  name: 'Accroute',
  components: { splitPane },
  props: {
    current: {
      type: Number,
      default: null
    }
  },
  data() {
    return {
      total: 0,
      listLoading: false,
      list: [],
      listQuery: {
        draw: 0,
        page: 0,
        pageSize: 5,
        itemId: '',
        keyId: this.current
      },
      columns: [
        { data: 'requestIdKey', name: '请求Key', searchable: true, orderable: true },
        { data: 'upstreamPathTemplate', name: '上游路径', searchable: true, orderable: true },
        { data: 'downstreamPathTemplate', name: '下游路径', searchable: true, orderable: true },
        { data: 'downstreamHostAndPorts', name: '下游地址', searchable: true, orderable: true }
      ],
      defaultProps: { children: 'children', label: 'label' },
      trees: []
    }
  },
  watch: {
    current: function(val, oldVal) {
      this.listQuery.keyId = val
      this.listQuery.draw = 0
      this.handleFilter()
    }
  },
  created() {
    this.handleFilter()
    getCascaderList().then(r => { this.trees = r.result })
  },
  methods: {
    getList() {
      this.listLoading = true
      this.list = []
      getAccGroupList(this.listQuery).then(response => {
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
    handleNodeClick(data) {
      this.listQuery.itemId = data.value
      this.getList()
    },
    handleAccessChange(status, row) {
      this.listLoading = true
      toAccReRoute({ keyId: this.listQuery.keyId, reRouteId: row.reRouteId, type: status ? 1 : 0 }).then(response => {
        this.listLoading = false
        this.handleFilter()
      }).catch(() => {
        this.listLoading = false
      })
    }
  }
}
</script>

<style  scoped>
  .ass-container {
    position: relative;
    margin: 0;
    height: 400px;
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
