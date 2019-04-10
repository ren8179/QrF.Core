import request from '@/utils/request'

const base_url = '/ReRoute/'

export function getCascaderList() {
  return request({
    url: base_url + 'GetCascaderList',
    method: 'get'
  })
}

export function getPageList(params) {
  return request({
    url: base_url + 'GetPageList',
    method: 'get',
    params
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

