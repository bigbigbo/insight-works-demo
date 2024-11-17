<template>
  <el-dialog
    v-model="dialogVisible"
    :title="isEdit ? '编辑设备' : '新增设备'"
    width="500px"
  >
    <el-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      label-width="100px"
    >
      <el-form-item label="设备编号" prop="equipmentCode">
        <el-input
          v-model="formData.equipmentCode"
          placeholder="请输入设备编号"
          maxlength="50"
        />
      </el-form-item>
      <el-form-item label="厂商" prop="manufacturerId">
        <el-select
          v-model="formData.manufacturerId"
          placeholder="请选择厂商"
          clearable
        >
          <el-option
            v-for="item in manufacturerOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="联系人" prop="contactPerson">
        <el-input
          v-model="formData.contactPerson"
          placeholder="请输入联系人"
          maxlength="50"
        />
      </el-form-item>
      <el-form-item label="联系电话" prop="contactPhone">
        <el-input
          v-model="formData.contactPhone"
          placeholder="请输入联系电话"
          maxlength="20"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="handleCancel">取消</el-button>
        <el-button type="primary" :loading="submitLoading" @click="handleSubmit"
          >确定</el-button
        >
      </span>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from "vue";
import { ElMessage } from "element-plus";
import { EquipmentService } from "@/core/services/equipment";
import { ManufacturerService } from "@/core/services/manufacturer";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  isEdit: {
    type: Boolean,
    default: false
  },
  data: {
    type: Object,
    default: () => ({})
  }
});

const emit = defineEmits(["update:visible", "success"]);

const dialogVisible = ref(false);
const formRef = ref();
const submitLoading = ref(false);
const manufacturerOptions = ref([]);

const formData = ref({
  id: "",
  equipmentCode: "",
  manufacturerId: "",
  contactPerson: "",
  contactPhone: ""
});

const formRules = {
  equipmentCode: [
    { required: true, message: "请输入设备编号", trigger: "blur" },
    { max: 50, message: "设备编号不能超过50个字符", trigger: "blur" }
  ],
  manufacturerId: [
    { required: true, message: "请选择厂商", trigger: "change" }
  ],
  contactPerson: [
    { max: 50, message: "联系人不能超过50个字符", trigger: "blur" }
  ],
  contactPhone: [
    { max: 20, message: "联系电话不能超过20个字符", trigger: "blur" }
  ]
};

const getManufacturerOptions = async () => {
  try {
    const res = await ManufacturerService.getList({
      pageSize: 999,
      pageIndex: 1
    });
    if (res.success) {
      manufacturerOptions.value = res.data.items.map(item => ({
        label: item.name,
        value: item.id
      }));
    }
  } catch (err) {
    console.error("获取厂商列表失败:", err);
  }
};

watch(
  () => props.visible,
  val => {
    dialogVisible.value = val;
  }
);

watch(
  () => dialogVisible.value,
  val => {
    emit("update:visible", val);
  }
);

watch(
  () => props.data,
  val => {
    // @ts-ignore
    formData.value = { ...val };
  },
  { deep: true }
);

const handleCancel = () => {
  dialogVisible.value = false;
  formRef.value?.resetFields();
};

const handleSubmit = async () => {
  if (!formRef.value) return;

  await formRef.value.validate(async (valid: boolean) => {
    if (valid) {
      try {
        submitLoading.value = true;
        const service = props.isEdit
          ? EquipmentService.update
          : EquipmentService.create;
        const res = await service(formData.value);
        if (res.success) {
          ElMessage.success(props.isEdit ? "更新成功" : "新增成功");
          dialogVisible.value = false;
          emit("success");
          formRef.value?.resetFields();
        } else {
          ElMessage.error(
            res.message || (props.isEdit ? "更新失败" : "新增失败")
          );
        }
      } catch (err) {
        ElMessage.error(props.isEdit ? "更新失败" : "新增失败");
      } finally {
        submitLoading.value = false;
      }
    }
  });
};

onMounted(() => {
  getManufacturerOptions();
});
</script>

<style scoped></style>
