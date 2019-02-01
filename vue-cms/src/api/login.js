import request from '@/utils/request'

export function loginByUsername(userName, password) {
  const data = {
    userName,
    password
  }
  return request({
    url: '/Account/Login',
    method: 'post',
    params: data
  })
}

export function logout() {
  return request({
    url: '/Account/Logout',
    method: 'get'
  })
}

export function getUserInfo(token) {
  return request({
    url: '/User/GetUserInfo',
    method: 'get',
    params: { token }
  })
}
