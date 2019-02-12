import request from '@/utils/request'

export function fetchList(data) {
  return request({
    url: '/Article/GetPageList',
    method: 'post',
    data
  })
}

export function fetchArticle(id) {
  return request({
    url: '/Article/GetById',
    method: 'get',
    params: { id }
  })
}

export function createArticle(data) {
  return request({
    url: '/Article/Create',
    method: 'post',
    data
  })
}

export function updateArticle(data) {
  return request({
    url: '/Article/Edit',
    method: 'post',
    data
  })
}

export function del(id) {
  return request({
    url: '/Article/Delete',
    method: 'get',
    params: { id }
  })
}

export function fetchPv(pv) {
  return request({
    url: '/article/pv',
    method: 'get',
    params: { pv }
  })
}

export function getArticleTypeTree() {
  return request({
    url: '/ArticleType/GetArticleTypeTree',
    method: 'get'
  })
}

export function delType(id) {
  return request({
    url: '/ArticleType/Delete',
    method: 'get',
    params: { id }
  })
}

export function createType(data) {
  return request({
    url: '/ArticleType/Create',
    method: 'post',
    data
  })
}

export function editType(data) {
  return request({
    url: '/ArticleType/Edit',
    method: 'post',
    data
  })
}

export function getTypeById(id) {
  return request({
    url: '/ArticleType/GetById',
    method: 'get',
    params: { id }
  })
}

export function getTypeList() {
  return request({
    url: '/ArticleType/GetTypeList',
    method: 'get'
  })
}

