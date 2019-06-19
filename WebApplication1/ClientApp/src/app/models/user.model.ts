interface IUsers {
  id: string;
  userName: string;
}
export class User implements IUsers{
  id: string;
  userName: string;
  private url;
  private http;
}
