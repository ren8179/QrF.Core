import request from '@/utils/request'

const base_url = '/ApiResource/'

export function getPageList(params) {
  return request({
    url: base_url + 'GetPageList',
    method: 'get',
    params
  })
}

export function getScopeList() {
  return request({
    url: base_url + 'GetScopeList',
    method: 'get'
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

