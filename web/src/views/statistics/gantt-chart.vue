<script setup lang="ts">
import { onMounted, ref } from "vue";
import { StatisticsService } from "@/core/services/statistics";
import { gantt } from "@/lib/dhtmlx-gantt/dhtmlxgantt.es";
import dayjs from "dayjs";
import { EquipmentService } from "@/core/services/equipment";
import type { EquipmentEntity } from "@/core/entities/equipment";

defineOptions({
  name: "GanttChart"
});

const equipmentList = ref<EquipmentEntity[]>([]);
const selectedEquipment = ref<string>("");
const dateRange = ref<[string, string]>([
  dayjs().startOf("year").format("YYYY-MM-DD"), // 修改为今年年初
  dayjs().endOf("year").format("YYYY-MM-DD") // 修改为今年年末
]);

// 时间跨度选项
const timeScaleOptions = [
  { label: "日", value: "day" },
  { label: "周", value: "week" },
  { label: "月", value: "month" },
  { label: "季度", value: "quarter" },
  { label: "年", value: "year" }
];
const selectedTimeScale = ref("month");

const formatStatusText = (status: number) => {
  const statusMap = {
    0: "正常",
    1: "异常",
    2: "警告"
  };
  return statusMap[status] || "未知状态";
};

const loadEquipmentList = async () => {
  try {
    const res = await EquipmentService.getList({
      pageIndex: 1,
      pageSize: 100
    });
    // @ts-ignore
    equipmentList.value = res.data.items;
    if (equipmentList.value.length > 0) {
      selectedEquipment.value = equipmentList.value[0].id;
      await loadGanttData();
    }
  } catch (error) {
    console.error("加载设备列表失败:", error);
  }
};

const setTimeScale = (value: string) => {
  switch (value) {
    case "day":
      gantt.config.scale_unit = "day";
      gantt.config.date_scale = "%d %M";
      gantt.config.subscales = [];
      gantt.config.min_column_width = 80;
      break;
    case "week":
      gantt.config.scale_unit = "week";
      gantt.config.date_scale = "%W周";
      gantt.config.subscales = [{ unit: "day", step: 1, date: "%D" }];
      gantt.config.min_column_width = 50;
      break;
    case "month":
      gantt.config.scale_unit = "month";
      gantt.config.date_scale = "%M";
      gantt.config.subscales = [{ unit: "week", step: 1, date: "%W周" }];
      gantt.config.min_column_width = 120;
      break;
    case "quarter":
      var monthScaleTemplate = function (date) {
        var quarter = Math.floor(date.getMonth() / 3) + 1;
        return date.getFullYear() + "年" + quarter + "季度";
      };
      gantt.config.scale_unit = "month";
      gantt.config.subscales = [
        { unit: "month", step: 3, format: monthScaleTemplate }
      ];
      gantt.config.min_column_width = 90;
      break;
    case "year":
      gantt.config.scale_unit = "year";
      gantt.config.date_scale = "%Y年";
      gantt.config.subscales = [{ unit: "month", step: 1, date: "%M" }];
      gantt.config.min_column_width = 200;
      break;
  }
  gantt.render();
};

const loadGanttData = async () => {
  if (!selectedEquipment.value) return;

  try {
    const res = await StatisticsService.getGanttChartData({
      equipmentId: selectedEquipment.value,
      startTime: dateRange.value[0],
      endTime: dateRange.value[1]
    });

    if (res.data && res.data.length > 0) {
      // 在加载新数据前清除旧数据
      gantt.clearAll();

      gantt.plugins({
        quick_info: true
      });

      // 设置日期格式为中文
      gantt.config.date_format = "%Y-%m-%d";
      gantt.config.xml_date = "%Y-%m-%d";
      gantt.i18n.setLocale("cn"); // 设置中文语言包

      // 禁用左侧任务列表和右侧条状图的交互
      gantt.config.readonly = true; // 设置为只读模式
      gantt.config.drag_links = false; // 禁用链接拖拽
      gantt.config.drag_progress = false; // 禁用进度条拖拽
      gantt.config.drag_resize = false; // 禁用任务大小调整
      gantt.config.drag_move = false; // 禁用任务移动
      gantt.config.drag_timeline = { ignore: true }; // 禁用时间线拖拽
      gantt.config.order_branch = false; // 禁用分支排序
      gantt.config.order_branch_free = false; // 禁用自由分支排序

      // 设置不同状态的任务颜色
      gantt.templates.task_class = (start, end, task) => {
        const status = task.text.split(" - ")[0];
        switch (status) {
          case "正常":
            return "status-normal";
          case "异常":
            return "status-error";
          case "警告":
            return "status-warning";
          default:
            return "";
        }
      };

      // 设置时间跨度
      setTimeScale(selectedTimeScale.value);

      // 只在第一次初始化甘特图
      if (!gantt.getTaskByTime().length) {
        gantt.init("gantt-chart");
      }

      // 使用普通数组而不是响应式数据
      const tasks = [];
      [res.data[0]].forEach(item => {
        const records = item.statusRecords;
        records.push({
          statusChangeTime: new Date()
        });
        for (let i = 0; i < records.length - 1; i++) {
          const current = records[i];
          const next = records[i + 1];
          tasks.push({
            id: `${item.equipmentCode}-${i}`,
            text: `${formatStatusText(current.status)} - ${current.executedBy}`,
            start_date: dayjs(current.statusChangeTime).format("YYYY-MM-DD"),
            duration: Number(
              dayjs(next.statusChangeTime).diff(
                dayjs(current.statusChangeTime),
                "day"
              )
            ),
            progress: 1
          });
        }
      });

      gantt.parse({
        tasks
      });
    }
  } catch (error) {
    console.error("加载甘特图数据失败:", error);
  }
};

const handleEquipmentChange = async () => {
  await loadGanttData();
};

const handleDateRangeChange = async () => {
  await loadGanttData();
};

const handleTimeScaleChange = () => {
  setTimeScale(selectedTimeScale.value);
};

onMounted(() => {
  loadEquipmentList();
});
</script>

<template>
  <div class="w-full h-[calc(100vh-144px)] bg-white rounded-lg p-4">
    <div class="mb-4 flex items-center gap-4">
      <div class="w-64">
        <el-select
          v-model="selectedEquipment"
          class="w-full"
          placeholder="请选择设备"
          @change="handleEquipmentChange"
        >
          <el-option
            v-for="equipment in equipmentList"
            :key="equipment.id"
            :label="equipment.equipmentCode"
            :value="equipment.id"
          />
        </el-select>
      </div>

      <div>
        <el-date-picker
          v-model="dateRange"
          type="daterange"
          range-separator="至"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          :default-value="[
            dayjs().startOf('year').toDate(), // 修改为今年年初
            dayjs().endOf('year').toDate() // 修改为今年年末
          ]"
          @change="handleDateRangeChange"
        />
      </div>

      <div>
        <el-radio-group
          v-model="selectedTimeScale"
          @change="handleTimeScaleChange"
        >
          <el-radio-button
            v-for="option in timeScaleOptions"
            :key="option.value"
            :value="option.value"
          >
            {{ option.label }}
          </el-radio-button>
        </el-radio-group>
      </div>
    </div>

    <div id="gantt-chart" style="width: 100%; height: calc(100% - 60px)" />
  </div>
</template>

<style>
@import "@/lib/dhtmlx-gantt/dhtmlxgantt.css";

/* 不同状态的任务颜色样式 */
.status-normal .gantt_task_progress {
  background: #4caf50;
}

.status-error .gantt_task_progress {
  background: #f44336;
}

.status-warning .gantt_task_progress {
  background: #ff9800;
}

.status-normal .gantt_task_content {
  color: #4caf50;
}

.status-error .gantt_task_content {
  color: #f44336;
}

.status-warning .gantt_task_content {
  color: #ff9800;
}
</style>
