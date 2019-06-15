interface IUsers {
  id: string;
  name: string;
}
export class User implements IUsers{
  id: string;
  name: string;
  private url;
  private http;
}
