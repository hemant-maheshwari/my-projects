export class Friend{
  id: number;
  userId: number;
  friendId: number;

  public constructor(init?: Partial<Friend>) {
    Object.assign(this, init);
  }
}
