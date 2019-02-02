import request from '@/utils/request'

export function getNavTree() {
  return request({
    url: '/Navigation/GetNavTree',
    method: 'get'
  })
}

export function delNav(id) {
  return request({
    url: '/Navigation/Delete',
    method: 'get',
    params: { id }
  })
}

export function createNav(data) {
  return request({
    url: '/Navigation/Create',
    method: 'post',
    data
  })
}

export function editNav(data) {
  return request({
    url: '/Navigation/Edit',
    method: 'post',
    data
  })
}

export function moveNav(id, parentId, position) {
  return request({
    url: '/Navigation/MoveNav',
    method: 'post',
    params: { id, parentId, position }
  })
}
