<template>
  <div class="createPost-container">
    <el-form ref="postForm" :model="postForm" :rules="rules" class="form-container">
      <sticky :class-name="'sub-navbar '+postForm.status">
        <el-button v-loading="loading" style="margin-left: 10px;" type="success" @click="submitForm">发布</el-button>
        <el-button v-loading="loading" type="warning" @click="draftForm">保存</el-button>
      </sticky>
      <div class="createPost-main-container">
        <el-row>
          <el-col :span="24">
            <el-form-item style="margin-bottom: 40px;" prop="title">
              <MDinput v-model="postForm.title" :maxlength="100" name="name" required>标题</MDinput>
            </el-form-item>
            <div class="postInfo-container">
              <el-row>
                <el-col :span="16">
                  <el-form-item label-width="110px" label="URL:">
                    <el-input :rows="1" v-model="postForm.url" type="textarea" class="article-textarea" autosize placeholder="请输入URL"/>
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <el-form-item label-width="80px" label="阅读次数:" class="postInfo-container-item">
                    <el-input v-model="postForm.counter" size="small" class="filter-item" placeholder="请输入阅读次数" />
                  </el-form-item>
                </el-col>
              </el-row>
            </div>
          </el-col>
        </el-row>
        <el-form-item style="margin-bottom: 40px;" label-width="110px" label="SEO关键字:">
          <el-input :rows="1" v-model="postForm.metaKeyWords" type="textarea" class="article-textarea" autosize placeholder="请输入SEO关键字"/>
        </el-form-item>
        <el-form-item style="margin-bottom: 40px;" label-width="110px" label="SEO描述:">
          <el-input :rows="1" v-model="postForm.metaDescription" type="textarea" class="article-textarea" autosize placeholder="请输入SEO描述"/>
        </el-form-item>
        <el-form-item style="margin-bottom: 40px;" label-width="110px" label="概述:">
          <el-input :rows="1" v-model="postForm.summary" type="textarea" class="article-textarea" autosize placeholder="请输入概述"/>
          <span v-show="contentShortLength" class="word-counter">{{ contentShortLength }}字</span>
        </el-form-item>
        <el-form-item prop="content" style="margin-bottom: 30px;">
          <Tinymce ref="editor" :height="400" v-model="postForm.articleContent" />
        </el-form-item>
        <el-form-item label-width="110px" label="文章类别:">
          <el-tree v-loading="loading" ref="typeTree" :data="treelist" :props="defaultProps" node-key="id" highlight-current default-expand-all />
        </el-form-item>
        <el-form-item label-width="110px" label="状态:">
          <el-select v-model="postForm.status" class="filter-item" placeholder="请选择">
            <el-option v-for="item in opts" :key="item.Id" :label="item.Title" :value="item.Id" />
          </el-select>
        </el-form-item>
        <el-form-item label-width="110px" prop="imageUrl" label="图片:" style="margin-bottom: 30px;">
          <Upload v-model="postForm.imageUrl" />
        </el-form-item>
        <el-form-item label-width="110px" prop="imageThumbUrl" label="缩略图:" style="margin-bottom: 30px;">
          <Upload v-model="postForm.imageThumbUrl" />
        </el-form-item>
      </div>
    </el-form>
  </div>
</template>

<script>
import Tinymce from '@/components/Tinymce'
import Upload from '@/components/Upload/singleImage'
import MDinput from '@/components/MDinput'
import Sticky from '@/components/Sticky' // 粘性header组件
import { fetchArticle, getArticleTypeTree, createArticle, updateArticle } from '@/api/article'

const defaultForm = {
  url: '',
  title: '', // 文章题目
  counter: 0,
  articleContent: '', // 文章内容
  metaKeyWords: '', // SEO关键字
  metaDescription: '', // SEO描述
  summary: '', // 概述
  id: undefined,
  articleTypeID: null,
  status: 1,
  isPublish: false,
  actionType: 1,
  imageUrl: '',
  imageThumbUrl: ''
}

export default {
  name: 'ArticleDetail',
  components: { Tinymce, MDinput, Upload, Sticky },
  props: {
    isEdit: {
      type: Boolean,
      default: false
    }
  },
  data() {
    const validateRequire = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('必填项，请输入'))
      } else {
        callback()
      }
    }
    return {
      postForm: Object.assign({}, defaultForm),
      loading: false,
      opts: [{ Id: 1, Title: '有效' }, { Id: 0, Title: '无效' }],
      treelist: [],
      defaultProps: { children: 'children', label: 'label' },
      rules: {
        title: [{ validator: validateRequire }],
        articleContent: [{ validator: validateRequire }]
      },
      tempRoute: {}
    }
  },
  computed: {
    contentShortLength() {
      return this.postForm.summary.length
    }
  },
  created() {
    this.getTrees()
    if (this.isEdit) {
      const id = this.$route.params && this.$route.params.id
      this.fetchData(id)
    } else {
      this.postForm = Object.assign({}, defaultForm)
    }
    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route)
  },
  methods: {
    fetchData(id) {
      fetchArticle(id).then(response => {
        this.postForm = response.data
        this.$nextTick(() => {
          this.$refs.typeTree.setCurrentKey(this.postForm.articleTypeID)
        })
        this.setTagsViewTitle()
      }).catch(err => {
        console.log(err)
      })
    },
    getTrees() {
      this.loading = true
      getArticleTypeTree().then(response => {
        this.loading = false
        this.treelist = response.data
        this.$nextTick(() => {
          this.$refs.typeTree.setCurrentKey(this.postForm.articleTypeID)
        })
      }).catch(() => {
        this.loading = false
      })
    },
    setTagsViewTitle() {
      const title = '编辑文章'
      const route = Object.assign({}, this.tempRoute, { title: `${title}-${this.postForm.id}` })
      this.$store.dispatch('updateVisitedView', route)
    },
    submitForm() {
      this.$refs.postForm.validate(valid => {
        if (valid) {
          this.loading = true
          const currkey = this.$refs.typeTree.getCurrentKey()
          if (currkey) {
            this.postForm.articleTypeID = currkey
          }
          this.postForm.actionType = 5
          const opt = this.isEdit ? updateArticle(this.postForm) : createArticle(this.postForm)
          opt.then(response => {
            this.loading = false
            this.$notify({ title: '成功', message: '发布文章成功', type: 'success', duration: 2000 })
          }).catch(err => {
            this.loading = false
            console.log(err)
          })
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    draftForm() {
      if (this.postForm.articleContent.length === 0 || this.postForm.title.length === 0) {
        this.$message({ message: '请填写必要的标题和内容', type: 'warning' })
        return
      }
      const currkey = this.$refs.typeTree.getCurrentKey()
      if (currkey) {
        this.postForm.articleTypeID = currkey
      }
      this.postForm.actionType = this.isEdit ? 2 : 1
      const opt = this.isEdit ? updateArticle(this.postForm) : createArticle(this.postForm)
      opt.then(response => {
        this.$notify({ title: '成功', message: '保存成功', type: 'success', duration: 2000 })
      })
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
@import "~@/styles/mixin.scss";
.createPost-container {
  position: relative;
  .createPost-main-container {
    padding: 40px 45px 20px 50px;
    .postInfo-container {
      position: relative;
      @include clearfix;
      margin-bottom: 10px;
      .postInfo-container-item {
        float: left;
      }
    }
  }
  .word-counter {
    width: 40px;
    position: absolute;
    right: -10px;
    top: 0px;
  }
}
</style>
