import {
  createEquipment,
  deleteEquipment,
  type EquipmentListParams,
  getEquipmentList,
  updateEquipment
} from "../datasources/equipment";
import { EquipmentEntity } from "../entities/equipment";

export class EquipmentService {
  static getList(params: EquipmentListParams) {
    return getEquipmentList(params).then(res => {
      res.data.items =
        res.data?.items?.map(item => new EquipmentEntity(item)) || [];
      return res;
    });
  }
  static create = createEquipment;
  static update = updateEquipment;
  static delete = deleteEquipment;
}
