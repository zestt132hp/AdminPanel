import { User } from '../models/user.model';

interface IAnnouncer {
  id: string;
  number: number;
  user: User;
  text: string;
  rate: number;
  creationDateTime: Date;
}

export class Announcer implements IAnnouncer {
  id: string;
  number: number;
  user: User;
  text: string;
  rate: number;
  creationDateTime: Date;
}
