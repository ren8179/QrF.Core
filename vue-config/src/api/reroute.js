import request from '@/utils/request'

const base_url = '/ReRoute/'

export function getCascaderList() {
  return request({
    url: base_url + 'GetCascaderList',
    method: 'get'
  })
}

export function editType(data) {
  return request({
    url: base_url + 'EditType',
    method: 'post',
    data
  })
}

export function delType(id) {
  return request({
    url: base_url + 'DelType',
    method: 'post',
    data: { id }
  })
}

export function getPageList(params) {
  return request({
    url: base_url + 'GetPageList',
    method: 'get',
    params
  })
}

export function getAccReRouteList(params) {
  return request({
    url: base_url + 'GetAccReRouteList',
    method: 'get',
    params
  })
}

export function toAccReRoute(data) {
  return request({
    url: base_url + 'ToAccReRoute',
    method: 'post',
    data
  })
}

export function edit(data) {
  return request({
    url: base_url + 'Edit',
    method: 'post',
    data
  })
}

export function del(id) {
  return request({
    url: base_url + 'Delete',
    method: 'post',
    data: { id }
  })
}
