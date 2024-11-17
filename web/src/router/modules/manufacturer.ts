// 最简代码，也就是这些字段必须有
export default {
  path: "/manufacturer",
  name: "Manufacturer",
  meta: {
    title: "厂商管理",
    icon: "ep:school",
    rank: 10
  },
  redirect: "/manufacturer/list",
  children: [
    {
      path: "/manufacturer/list",
      name: "ManufacturerList",
      component: () => import("@/views/manufacturer/list.vue"),
      meta: {
        title: "厂商列表",
        showParent: true
      }
    }
  ]
} satisfies RouteConfigsTable;
