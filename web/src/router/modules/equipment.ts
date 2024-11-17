// 最简代码，也就是这些字段必须有
export default {
  path: "/equipment",
  name: "Equipment",
  meta: {
    title: "设备管理",
    icon: "ep:coin",
    rank: 20
  },
  redirect: "/equipment/list",
  children: [
    {
      path: "/equipment/list",
      name: "EquipmentList",
      component: () => import("@/views/equipment/list.vue"),
      meta: {
        title: "设备列表",
        showParent: true
      }
    },
    {
      path: "/equipment/data-sync",
      name: "EquipmentDataSync",
      component: () => import("@/views/equipment/data-sync.vue"),
      meta: {
        title: "数据同步",
        showParent: true
      }
    }
  ]
} satisfies RouteConfigsTable;
