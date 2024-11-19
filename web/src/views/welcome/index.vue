<script setup lang="ts">
import { useNav } from "@/layout/hooks/useNav";
import { EquipmentStatus } from "@/typings/api";
import { useTransition } from "@vueuse/core";
import { ref } from "vue";

const { userAvatar, username } = useNav();
const useTransitionValue = (value: number, duration = 1500) => {
  const source = ref(0);
  const outputValue = useTransition(source, {
    duration
  });
  source.value = value;
  return outputValue;
};

const indicator1 = useTransitionValue(36);
const indicator2 = useTransitionValue(1720);
const indicator3 = useTransitionValue(268500);
const indicator4 = useTransitionValue(89850000);

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

const equipmentStatusList = [
  {
    name: "CNC-2000",
    status: EquipmentStatus.Value0
  },
  {
    name: "ROB-3000",
    status: EquipmentStatus.Value0
  },
  {
    name: "ROB-3000",
    status: EquipmentStatus.Value1
  },
  {
    name: "ASM-1500",
    status: EquipmentStatus.Value0
  },
  {
    name: "QC-800",
    status: EquipmentStatus.Value2
  },
  {
    name: "CNC-2000",
    status: EquipmentStatus.Value0
  },
  {
    name: "CNC-2000",
    status: EquipmentStatus.Value0
  }
];

defineOptions({
  name: "Welcome"
});
</script>

<template>
  <div>
    <div class="flex items-center bg-white p-4 rounded-lg shadow-sm mb-4">
      <img class="w-20 h-20 rounded-full mr-4" :src="userAvatar" />
      <div>
        <p class="text-2xl">下午好，{{ username }}，祝你开心每一天！</p>
      </div>
    </div>
    <div class="flex justify-between">
      <el-statistic
        class="flex-1 bg-white p-4 rounded-lg shadow-sm mr-4"
        title="已投产设备数量"
        :value="indicator1"
      />
      <el-statistic
        class="flex-1 bg-white p-4 rounded-lg shadow-sm mr-4"
        title="已投产产品型号"
        :value="indicator2"
      />
      <el-statistic
        class="flex-1 bg-white p-4 rounded-lg shadow-sm mr-4"
        title="已生产批次数"
        :value="indicator3"
      />
      <el-statistic
        class="flex-1 bg-white p-4 rounded-lg shadow-sm"
        title="本周生产总值"
        :value="indicator4"
      />
    </div>

    <div class="grid grid-cols-4 gap-4">
      <div class="col-span-3 mt-4 bg-white p-4 rounded-lg shadow-sm">
        <div class="flex justify-between items-center mb-4">
          <h2 class="mb-0 text-lg font-medium">进行中的项目</h2>
          <a class="text-sm text-blue-500">查看更多</a>
        </div>
        <div class="grid grid-cols-2 gap-4">
          <div
            class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow"
          >
            <h3 class="text-base font-medium text-gray-900 mb-2">
              智能制造项目A
            </h3>
            <div class="space-y-2">
              <div class="flex items-center">
                <span class="text-gray-500 w-24">设备名称:</span>
                <span class="text-gray-900">CNC-2000</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">产品型号:</span>
                <span class="text-gray-900">XJ-238</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">开始时间:</span>
                <el-tag size="small" type="success">2024-01-15</el-tag>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">预计结束:</span>
                <el-tag size="small" type="warning">2024-03-15</el-tag>
              </div>
            </div>
          </div>

          <div
            class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow"
          >
            <h3 class="text-base font-medium text-gray-900 mb-2">
              自动化生产线B
            </h3>
            <div class="space-y-2">
              <div class="flex items-center">
                <span class="text-gray-500 w-24">设备名称:</span>
                <span class="text-gray-900">ROB-3000</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">产品型号:</span>
                <span class="text-gray-900">AX-892</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">开始时间:</span>
                <el-tag size="small" type="success">2024-01-20</el-tag>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">预计结束:</span>
                <el-tag size="small" type="warning">2024-04-20</el-tag>
              </div>
            </div>
          </div>

          <div
            class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow"
          >
            <h3 class="text-base font-medium text-gray-900 mb-2">
              智能装配项目C
            </h3>
            <div class="space-y-2">
              <div class="flex items-center">
                <span class="text-gray-500 w-24">设备名称:</span>
                <span class="text-gray-900">ASM-1500</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">产品型号:</span>
                <span class="text-gray-900">BT-567</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">开始时间:</span>
                <el-tag size="small" type="success">2024-02-01</el-tag>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">预计结束:</span>
                <el-tag size="small" type="warning">2024-05-01</el-tag>
              </div>
            </div>
          </div>

          <div
            class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow"
          >
            <h3 class="text-base font-medium text-gray-900 mb-2">质检系统D</h3>
            <div class="space-y-2">
              <div class="flex items-center">
                <span class="text-gray-500 w-24">设备名称:</span>
                <span class="text-gray-900">QC-800</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">产品型号:</span>
                <span class="text-gray-900">CT-123</span>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">开始时间:</span>
                <el-tag size="small" type="success">2024-02-10</el-tag>
              </div>
              <div class="flex items-center">
                <span class="text-gray-500 w-24">预计结束:</span>
                <el-tag size="small" type="warning">2024-04-10</el-tag>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-span-1 mt-4 bg-white p-4 rounded-lg shadow-sm">
        <el-table :data="equipmentStatusList" style="width: 100%">
          <el-table-column prop="name" label="设备名称" />
          <el-table-column prop="status" label="当前机况">
            <template #default="scope">
              <el-tag :type="statusMap[scope.row.status].type">
                {{ statusMap[scope.row.status].label }}
              </el-tag>
            </template>
          </el-table-column>
        </el-table>
      </div>
    </div>
  </div>
</template>

<style scoped>
.el-col {
  text-align: center;
}
</style>
