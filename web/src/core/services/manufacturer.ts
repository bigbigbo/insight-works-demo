import {
  createManufacturer,
  deleteManufacturer,
  getManufacturerList,
  type ManufacturerListParams,
  updateManufacturer
} from "@/core/datasources/manufacturer";
import { ManufacturerEntity } from "@/core/entities/manufacturer";

export class ManufacturerService {
  static getList = (params: ManufacturerListParams) => {
    return getManufacturerList(params).then(res => {
      res.data.items =
        res.data?.items?.map(item => new ManufacturerEntity(item)) || [];
      return res;
    });
  };
  static create = createManufacturer;
  static update = updateManufacturer;
  static delete = deleteManufacturer;
}
