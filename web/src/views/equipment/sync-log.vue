<script setup lang="ts">
import { h, ref } from "vue";
import { ElTag } from "element-plus";

import QueryTable from "@/components/query-table/index.vue";
import { EquipmentSyncService } from "@/core/services/equipment-sync";
import { SyncType, SyncStatus } from "@/typings/api";

defineOptions({
  name: "EquipmentSyncLog"
});

const loading = ref(false);

const columns = [
  {
    prop: "equipment.equipmentCode",
    label: "设备编号",
    width: 120
  },
  {
    prop: "syncType",
    label: "同步类型",
    width: 120,
    formatter: (row: { syncType: SyncType }) => {
      const typeMap = {
        [SyncType.Value0]: "机况数据",
        [SyncType.Value1]: "生产数据"
      };
      return typeMap[row.syncType] || "-";
    }
  },
  {
    prop: "formattedSyncStartTime",
    label: "开始时间",
    width: 180
  },
  {
    prop: "formattedSyncEndTime",
    label: "结束时间",
    width: 180
  },
  {
    prop: "status",
    label: "状态",
    width: 100,
    formatter: (row: { status: SyncStatus }) => {
      const statusMap = {
        [SyncStatus.Value0]: {
          type: "danger",
          label: "同步失败"
        },
        [SyncStatus.Value1]: {
          type: "success",
          label: "同步成功"
        }
      };
      const status = statusMap[row.status];
      // @ts-ignore
      return status ? h(ElTag, { type: status.type }, () => status.label) : "-";
    }
  },
  {
    prop: "errorMessage",
    label: "错误信息",
    minWidth: 200
  }
];
</script>

<template>
  <div class="sync-log">
    <QueryTable
      v-model:loading="loading"
      :api="EquipmentSyncService.getList"
      :columns="columns"
      stripe
      show-index
    >
      <template #default>
        <el-table-column
          v-for="col in columns"
          :key="col.prop"
          :prop="col.prop"
          :label="col.label"
          :width="col.width"
          :min-width="col.minWidth"
          :formatter="col.formatter"
          show-overflow-tooltip
        />
      </template>
    </QueryTable>
  </div>
</template>

<style lang="scss" scoped>
.sync-log {
  padding: 20px;
  background: #fff;
  border-radius: 4px;
}
</style>
