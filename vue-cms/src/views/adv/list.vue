<template>
  <div class="components-container">
    <split-pane :default-percent="15" split="vertical">
      <template slot="paneL">
        <div class="left-container">
          <div class="tree-nav-title">广告栏位</div>
          <el-menu default-active="2" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose">
            <el-menu-item v-for="item in types" :key="item.iD" index="2"><i class="el-icon-menu" /><span slot="title">导航二</span></el-menu-item>
          </el-menu>
        </div>
      </template>
      <template slot="paneR">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <!-- <el-button v-waves type="primary" class="filter-item" icon="el-icon-search" size="small" style="float:right;" @click="handleFilter">查询</el-button> -->
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
  </div>
</template>

<script>
import splitPane from 'vue-splitpane'
import { getClassList } from '@/api/adv'
import { fetchList, del } from '@/api/article'
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
      listQuery: {
        draw: 1,
        start: 0,
        length: 10,
        columns: [
          { data: 'title', name: '标题', searchable: true, orderable: true },
          { data: 'articleTypeID', name: '文章类别', searchable: true, orderable: true, search: { regex: false, opeartor: 'Equal' }},
          { data: 'isPublish', name: '已发布', searchable: true, orderable: true },
          { data: 'createbyName', name: '创建人', searchable: true, orderable: true },
          { data: 'createDate', name: '创建日期', searchable: true, orderable: true }
        ]
      },
      listLoading: true,
      btns: [
        { DomId: 'btnAdd', Name: '新增', Type: 'button', Class: 'primary', Icon: 'plus' },
        { DomId: 'btnEdit', Name: '编辑', Type: 'inline', Class: 'success', Icon: 'edit' },
        { DomId: 'btnDel', Name: '删除', Type: 'inline', Class: 'danger', Icon: 'delete' }
      ]
    }
  },
  created() {
    this.handleFilter()
    getClassList().then(r => { this.types = r.data })
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
            const type = this.types.filter(o => { return o.id === item.articleTypeID })
            if (type && type.length > 0) {
              item.articleTypeID = type[0].title
            }
            item.isPublish = item.isPublish ? '是' : '否'
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
          this.$router.push('/article/create')
          break
        case 'btnEdit':
          this.$router.push('/article/edit/' + row.id)
          break
        case 'btnDel':
          this.handleDelete(row)
          break
      }
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
    }
  }
}
</script>
