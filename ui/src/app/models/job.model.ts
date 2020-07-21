import { CustomerModel } from 'src/app/models/customer.model';
export interface JobModel {
  jobId: number;
  engineer: string;
  customer: CustomerModel;
  when: Date;
}
