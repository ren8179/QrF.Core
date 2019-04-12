import request from '@/utils/request'

const base_url = '/AuthGroup/'

export function getPageList(params) {
  return request({
    url: base_url + 'GetPageList',
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

