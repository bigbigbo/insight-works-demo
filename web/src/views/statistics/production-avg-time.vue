<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import * as echarts from "echarts";

import { ProductModelService } from "@/core/services/product-model";
import { StatisticsService } from "@/core/services/statistics";

defineOptions({
  name: "ProductionAvgTime"
});

// 设置默认时间范围为今年
const currentYear = new Date().getFullYear();
const defaultDateRange: [string, string] = [
  `${currentYear}-01-01`,
  `${currentYear}-12-31`
];
const dateRange = ref<[string, string]>(defaultDateRange);
const selectedModels = ref<string[]>([]);
const modelList = ref([]);

// 为每个型号生成固定的颜色并保存
const modelColors = ref<Record<string, string>>({});

let chartInstance: echarts.ECharts | null = null;

const getModelList = async () => {
  try {
    const res = await ProductModelService.getList({
      pageIndex: 1,
      pageSize: 100
    });
    modelList.value = res.data?.items || [];
    selectedModels.value = modelList.value.map(model => model.id);

    modelList.value.forEach(model => {
      if (!modelColors.value[model.modelCode]) {
        modelColors.value[model.modelCode] =
          `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 0.8)`;
      }
    });
  } catch (err) {
    console.error("获取产品型号列表失败:", err);
  }
};

const initChart = () => {
  const chartDom = document.getElementById("avgTimeChart");
  if (!chartDom) return;
  chartInstance = echarts.init(chartDom);
};

const updateChart = async () => {
  if (!chartInstance) return;

  try {
    const params = {
      modelIds: selectedModels.value,
      startTime: dateRange.value[0],
      endTime: dateRange.value[1]
    };

    const res = await StatisticsService.getProductionAvgTime(params);
    const data = res.data || [];

    const allEquipments = new Set<string>();
    data.forEach(model => {
      model.equipmentStats.forEach(stat => {
        allEquipments.add(stat.equipmentCode);
      });
    });
    const equipments = Array.from(allEquipments);

    const series = data.map(model => {
      return {
        name: model.modelCode,
        type: "bar",
        itemStyle: {
          color: modelColors.value[model.modelCode]
        },
        data: equipments.map(code => {
          const stat = model.equipmentStats.find(s => s.equipmentCode === code);
          return {
            value: stat ? stat.avgProductionTime : 0,
            avgTime: stat ? stat.avgProductionTime : 0,
            batchCount: stat ? stat.batchCount : 0
          };
        })
      };
    });

    const option = {
      tooltip: {
        show: true,
        trigger: "item",
        axisPointer: {
          type: "shadow"
        },
        formatter: function (param: any) {
          const modelData = param.data;
          return `${param.name}<br/>
                 ${param.marker} ${param.seriesName}<br/>
                 平均生产时间: ${modelData.avgTime}小时<br/>
                 总批次数量: ${modelData.batchCount}`;
        },
        textStyle: {
          fontSize: 14,
          color: "#ffffff"
        },
        backgroundColor: "rgba(50,50,50,0.7)",
        borderColor: "#333",
        borderWidth: 0,
        padding: [5, 10]
      },
      legend: {
        top: "5%",
        data: data.map(model => model.modelCode)
      },
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true
      },
      xAxis: {
        type: "category",
        data: equipments
      },
      yAxis: [
        {
          type: "value",
          name: "平均生产时间(小时)"
        }
      ],
      series: series
    };

    chartInstance.setOption(option, true);
  } catch (err) {
    console.error("获取生产时间统计数据失败:", err);
  }
};

const handleModelChange = () => {
  updateChart();
};

const handleDateChange = () => {
  updateChart();
};

onMounted(async () => {
  await getModelList();
  initChart();
  updateChart();
  window.addEventListener("resize", () => chartInstance?.resize());
});

onUnmounted(() => {
  window.removeEventListener("resize", () => chartInstance?.resize());
  if (chartInstance) {
    chartInstance.dispose();
    chartInstance = null;
  }
});
</script>

<template>
  <div class="bg-white p-4 rounded-lg">
    <div class="flex">
      <div class="w-56 pr-4 border-r border-gray-200">
        <h3 class="mb-4 text-lg font-medium">产品型号</h3>
        <el-checkbox-group v-model="selectedModels" @change="handleModelChange">
          <div class="flex flex-col gap-2">
            <el-checkbox
              v-for="model in modelList"
              :key="model.id"
              :value="model.id"
            >
              {{ model.modelCode }}
            </el-checkbox>
          </div>
        </el-checkbox-group>
      </div>

      <div class="flex-1 pl-4">
        <div class="mb-4">
          <el-date-picker
            v-model="dateRange"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            @change="handleDateChange"
          />
        </div>
        <div id="avgTimeChart" class="w-full" style="height: 500px" />
      </div>
    </div>
  </div>
</template>
