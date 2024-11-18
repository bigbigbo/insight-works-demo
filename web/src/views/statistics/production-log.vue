<script setup lang="ts">
import { ref, nextTick, watch } from "vue";
import { ElTree } from "element-plus";
import { EquipmentService } from "@/core/services/equipment";
import { StatisticsService } from "@/core/services/statistics";
import { ProductModelService } from "@/core/services/product-model";
import QueryTable from "@/components/query-table/index.vue";
import type { Equipment } from "@/typings/api";

defineOptions({
  name: "ProductionLog"
});

// 产品型号树相关
const modelList = ref<any[]>([]);
const selectedModel = ref<string>();

const getModelList = async () => {
  const res = await ProductModelService.getList({
    pageIndex: 1,
    pageSize: 999
  });
  if (res.success) {
    modelList.value = [
      {
        id: "",
        modelCode: "全部型号",
        children: res.data?.items || []
      }
    ];
  }
};

const handleNodeClick = async (data: any) => {
  selectedModel.value = data.id === "" ? undefined : data.modelCode;
  await nextTick();
  tableRef.value?.refresh();
};

// 查询条件相关
const dateRange = ref<[string, string]>(["", ""]);
const queryForm = ref({
  startTime: "",
  endTime: "",
  equipmentId: "",
  batchNumber: ""
});

watch(dateRange, newVal => {
  queryForm.value.startTime = newVal[0];
  queryForm.value.endTime = newVal[1];
});

const equipmentOptions = ref<Equipment[]>([]);

const getEquipmentList = async () => {
  const res = await EquipmentService.getList({
    pageIndex: 1,
    pageSize: 999
  });
  if (res.success) {
    equipmentOptions.value = res.data?.items || [];
  }
};

// 表格相关
const tableRef = ref();
const loading = ref(false);

const columns = [
  {
    prop: "equipmentCode",
    label: "设备编号",
    width: 120,
    fixed: "left"
  },
  {
    prop: "manufacturerName",
    label: "制造商",
    width: 150
  },
  {
    prop: "modelCode",
    label: "产品型号",
    minWidth: 120
  },
  {
    prop: "batchNumber",
    label: "批次号",
    minWidth: 120
  },
  {
    prop: "preLength",
    label: "加工前长度",
    width: 120
  },
  {
    prop: "preWidth",
    label: "加工前宽度",
    width: 120
  },
  {
    prop: "preHeight",
    label: "加工前高度",
    width: 120
  },
  {
    prop: "postLength",
    label: "加工后长度",
    width: 120
  },
  {
    prop: "postWidth",
    label: "加工后宽度",
    width: 120
  },
  {
    prop: "postHeight",
    label: "加工后高度",
    width: 120
  },
  {
    prop: "productionStartTime",
    label: "开始时间",
    width: 180
  },
  {
    prop: "productionEndTime",
    label: "结束时间",
    width: 180,
    fixed: "right"
  }
];

const getTableParams = () => {
  const params: Record<string, any> = {
    calculateAvgTime: false,
    ...queryForm.value
  };
  if (selectedModel.value) {
    params.modelCode = selectedModel.value;
  }
  return params;
};

const handleSearch = () => {
  tableRef.value?.refresh();
};

const handleReset = () => {
  dateRange.value = ["", ""];
  queryForm.value = {
    startTime: "",
    endTime: "",
    equipmentId: "",
    batchNumber: ""
  };
  handleSearch();
};

// const summaryMethod = ({
//   columns,
//   data,
//   summaryData
// }: {
//   columns: any[];
//   data: any[];
//   summaryData: {
//     avgProductionTime: number;
//   };
// }) => {
//   console.log(summaryData);
//   const sums: string[] = [];
//   columns.forEach((column, index) => {
//     if (index === 0) {
//       sums[index] = "平均生产时间";
//       return;
//     }
//     if (index === columns.length - 1) {
//       sums[index] =
//         `${((summaryData?.avgProductionTime || 0) / 60 / 60).toFixed(2)}小时`;
//       return;
//     }
//     sums[index] = "";
//   });
//   return sums;
// };

getModelList();
getEquipmentList();
</script>

<template>
  <div class="bg-white p-4 rounded-lg flex flex-col h-full">
    <div class="flex flex-1">
      <div class="equipment-tree">
        <el-tree
          :data="modelList"
          node-key="id"
          :props="{
            label: 'modelCode'
          }"
          :default-expanded-keys="['']"
          :expand-on-click-node="false"
          @node-click="handleNodeClick"
        />
      </div>
      <div class="table-container">
        <el-form :inline="true" :model="queryForm">
          <el-form-item label="时间范围">
            <el-date-picker
              v-model="dateRange"
              type="datetimerange"
              range-separator="-"
              start-placeholder="开始时间"
              end-placeholder="结束时间"
              style="width: 380px"
            />
          </el-form-item>
          <el-form-item label="设备编号">
            <el-select
              v-model="queryForm.equipmentId"
              placeholder="请选择设备"
              style="width: 200px"
            >
              <el-option
                v-for="item in equipmentOptions"
                :key="item.id"
                :label="item.equipmentCode"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="批次号">
            <el-input
              v-model="queryForm.batchNumber"
              placeholder="请输入批次号"
              style="width: 200px"
            />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSearch">查询</el-button>
            <el-button @click="handleReset">重置</el-button>
          </el-form-item>
        </el-form>
        <query-table
          ref="tableRef"
          v-model:loading="loading"
          :api="StatisticsService.getProductionLog"
          :params="getTableParams()"
          :columns="columns"
        >
          <template v-for="col in columns" :key="col.prop">
            <el-table-column v-bind="col" />
          </template>
        </query-table>
      </div>
    </div>
  </div>
</template>

<style scoped>
.production-log {
  display: flex;
  height: 100%;
}

.equipment-tree {
  width: 250px;
  padding: 10px;
  border-right: 1px solid #dcdfe6;
}

.table-container {
  flex: 1;
  padding: 10px;
  overflow-x: auto;
}
</style>
