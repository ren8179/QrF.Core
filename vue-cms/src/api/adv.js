import request from '@/utils/request'

export function getClassList() {
  return request({
    url: '/AdvClass/GetList',
    method: 'get'
  })
}
