// 最简代码，也就是这些字段必须有
export default {
  path: "/statistics",
  name: "Statistics",
  meta: {
    title: "统计分析",
    icon: "ep:data-analysis",
    rank: 40
  },
  children: [
    {
      path: "/statistics/equipment-status-log",
      name: "EquipmentStatusLog",
      component: () => import("@/views/statistics/equipment-status-log.vue"),
      meta: {
        title: "机况记录",
        showParent: true
      }
    },
    {
      path: "/statistics/production-log",
      name: "ProductionLog",
      component: () => import("@/views/statistics/production-log.vue"),
      meta: {
        title: "生产记录",
        showParent: true
      }
    },
    {
      path: "/statistics/gantt-chart",
      name: "GanttChart",
      component: () => import("@/views/statistics/gantt-chart.vue"),
      meta: {
        title: "甘特图",
        showParent: true
      }
    },
    {
      path: "/statistics/production-avg-time",
      name: "ProductionAvgTime",
      component: () => import("@/views/statistics/production-avg-time.vue"),
      meta: {
        title: "平均生产时间",
        showParent: true
      }
    }
  ]
} satisfies RouteConfigsTable;
