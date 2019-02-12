import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/views/layout/Layout'

/** note: Submenu only appear when children.length>=1
 *  detail see  https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 **/

/**
* hidden: true                   if `hidden:true` will not show in the sidebar(default is false)
* alwaysShow: true               if set true, will always show the root menu, whatever its child routes length
*                                if not set alwaysShow, only more than one route under the children
*                                it will becomes nested mode, otherwise not show the root menu
* redirect: noredirect           if `redirect:noredirect` will no redirect in the breadcrumb
* name:'router-name'             the name is used by <keep-alive> (must set!!!)
* meta : {
    roles: ['admin','editor']    will control the page roles (you can set multiple roles)
    title: 'title'               the name show in submenu and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    noCache: true                if true, the page will no be cached(default is false)
    breadcrumb: false            if false, the item will hidden in breadcrumb(default is true)
  }
**/
export const constantRouterMap = [{
  path: '/redirect',
  component: Layout,
  hidden: true,
  children: [{
    path: '/redirect/:path*',
    component: () =>
                import('@/views/redirect/index')
  }]
},
{
  path: '/login',
  component: () =>
            import('@/views/login/index'),
  hidden: true
},
{
  path: '/auth-redirect',
  component: () =>
            import('@/views/login/authredirect'),
  hidden: true
},
{
  path: '/404',
  component: () =>
            import('@/views/errorPage/404'),
  hidden: true
},
{
  path: '/401',
  component: () =>
            import('@/views/errorPage/401'),
  hidden: true
},
{
  path: '',
  component: Layout,
  redirect: 'dashboard',
  children: [{
    path: 'dashboard',
    component: () =>
                import('@/views/dashboard/index'),
    name: 'Dashboard',
    meta: { title: 'dashboard', icon: 'dashboard', noCache: true }
  }]
},
{
  path: '/navigation',
  component: Layout,
  redirect: '/navigation/list',
  name: 'Navigation',
  meta: {
    title: '导航管理',
    icon: 'navigation'
  },
  children: [{
    path: 'list',
    component: () =>
                import('@/views/navigation/list'),
    name: 'NavigationList',
    meta: { title: '导航管理', icon: 'guide' }
  }]
},
{
  path: '/article',
  component: Layout,
  redirect: '/article/typelist',
  name: 'Article',
  meta: {
    title: '文章管理',
    icon: 'documentation'
  },
  children: [{
    path: 'typelist',
    component: () =>
                import('@/views/article/typelist'),
    name: 'articletypelist',
    meta: { title: '文章类别', icon: 'component' }
  },
  {
    path: 'list',
    component: () =>
                import('@/views/article/list'),
    name: 'articlelist',
    meta: { title: '文章列表', icon: 'list' }
  },
  {
    path: 'create',
    component: () =>
                    import('@/views/article/create'),
    name: 'CreateArticle',
    meta: { title: '创建文章', icon: 'edit' }
  },
  {
    path: 'edit/:id(\\d+)',
    component: () =>
                    import('@/views/article/edit'),
    name: 'EditArticle',
    meta: { title: '编辑文章', noCache: true },
    hidden: true
  }]
}
]

export default new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRouterMap
})

export const asyncRouterMap = [{ path: '*', redirect: '/404', hidden: true }]
