import {Share} from './share';

export class Transaction{
  id: number;
  userId: number;
  amount: number;
  dateCreated: Date;
  dateUpdated: Date;
  title: string;
  type: string;
  shares: Share[];
}
