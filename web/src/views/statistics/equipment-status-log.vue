<script setup lang="ts">
import { h, ref, onMounted } from "vue";
import { ElTag, ElTree } from "element-plus";
import dayjs from "dayjs";

import QueryTable from "@/components/query-table/index.vue";
import { StatisticsService } from "@/core/services/statistics";
import { EquipmentService } from "@/core/services/equipment";
import type { EquipmentStatusHistory } from "@/typings/api";
import { EquipmentStatus } from "@/typings/api";

defineOptions({
  name: "EquipmentStatusLog"
});

const loading = ref(false);
const equipmentList = ref([]);
const tableRef = ref();
const queryParams = ref({
  equipmentId: "",
  startTime: "",
  endTime: "",
  status: "",
  executedBy: ""
});
const statusOptions = [
  {
    value: EquipmentStatus.Value0,
    label: "正常"
  },
  {
    value: EquipmentStatus.Value1,
    label: "警告"
  },
  {
    value: EquipmentStatus.Value2,
    label: "异常"
  }
];

const columns = [
  {
    prop: "equipment.equipmentCode",
    label: "设备编号",
    minWidth: 120
  },
  {
    prop: "status",
    label: "设备状态",
    width: 100,
    formatter: (row: EquipmentStatusHistory) => {
      const statusMap = {
        [EquipmentStatus.Value0]: {
          label: "正常",
          type: "success"
        },
        [EquipmentStatus.Value1]: {
          label: "警告",
          type: "warning"
        },
        [EquipmentStatus.Value2]: {
          label: "异常",
          type: "danger"
        }
      };
      const status = statusMap[row.status];
      return h(ElTag, { type: status.type as any }, () => status.label);
    }
  },
  {
    prop: "statusChangeTime",
    label: "状态变更时间",
    width: 180,
    formatter: (row: EquipmentStatusHistory) => {
      return dayjs(row.statusChangeTime).format("YYYY-MM-DD HH:mm:ss");
    }
  },
  {
    prop: "executedBy",
    label: "执行人",
    width: 120
  }
];

const getEquipmentList = async () => {
  const res = await EquipmentService.getList({
    pageIndex: 1,
    pageSize: 999
  });
  if (res.data?.items) {
    equipmentList.value = [
      {
        id: "",
        label: "全部设备",
        children: res.data.items.map((item: any) => ({
          id: item.id,
          label: `${item.manufacturer.name}-${item.equipmentCode}`
        }))
      }
    ];
  }
};

const handleNodeClick = (data: any, node: any) => {
  if (data.id === "") {
    node.expanded = true;
  }
  queryParams.value.equipmentId = data.id;
  handleQuery();
};

const handleQuery = () => {
  tableRef.value?.refresh();
};

const handleReset = () => {
  queryParams.value = {
    equipmentId: "",
    startTime: "",
    endTime: "",
    status: "",
    executedBy: ""
  };
  handleQuery();
};

onMounted(() => {
  getEquipmentList();
});
</script>

<template>
  <div class="app-container">
    <div class="bg-white p-4 rounded-lg">
      <div class="flex">
        <div class="w-56 border-r border-gray-200">
          <el-tree
            :data="equipmentList"
            :props="{ label: 'label' }"
            default-expand-all
            @node-click="handleNodeClick"
          />
        </div>

        <div class="flex-1 p-3">
          <el-form ref="queryRef" :model="queryParams" :inline="true">
            <el-form-item label="设备状态" prop="status">
              <el-select
                v-model="queryParams.status"
                placeholder="请选择设备状态"
                clearable
                style="width: 180px"
              >
                <el-option
                  v-for="item in statusOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="执行人" prop="executedBy">
              <el-input
                v-model="queryParams.executedBy"
                placeholder="请输入执行人"
                clearable
                style="width: 180px"
              />
            </el-form-item>
            <el-form-item label="时间范围" prop="timeRange">
              <el-date-picker
                v-model="queryParams.startTime"
                type="datetime"
                placeholder="开始时间"
                style="width: 180px"
              />
              <span class="mx-2">-</span>
              <el-date-picker
                v-model="queryParams.endTime"
                type="datetime"
                placeholder="结束时间"
                style="width: 180px"
              />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="handleQuery">查询</el-button>
              <el-button @click="handleReset">重置</el-button>
            </el-form-item>
          </el-form>

          <QueryTable
            ref="tableRef"
            v-model:loading="loading"
            :api="StatisticsService.getEquipmentStatusLog"
            :params="queryParams"
            :columns="columns"
            show-index
            stripe
          >
            <el-table-column
              v-for="col in columns"
              :key="col.prop"
              :prop="col.prop"
              :label="col.label"
              :width="col.width"
              :formatter="col.formatter"
            />
          </QueryTable>
        </div>
      </div>
    </div>
  </div>
</template>
