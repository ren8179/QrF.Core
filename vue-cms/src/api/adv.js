import request from '@/utils/request'

export function getClassList() {
  return request({
    url: '/AdvClass/GetList',
    method: 'get'
  })
}

export function fetchList(data) {
  return request({
    url: '/AdvList/GetPageList',
    method: 'post',
    data
  })
}

export function del(id) {
  return request({
    url: '/AdvList/Delete',
    method: 'get',
    params: { id }
  })
}

export function getAdv(id) {
  return request({
    url: '/AdvList/GetById',
    method: 'get',
    params: { id }
  })
}

export function create(data) {
  return request({
    url: '/AdvList/Create',
    method: 'post',
    data
  })
}

export function update(data) {
  return request({
    url: '/AdvList/Edit',
    method: 'post',
    data
  })
}
