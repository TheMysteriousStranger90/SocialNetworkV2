import { User } from "./user";

export class UserParams {
  gender: string;
  specializationId = 0;
  minAge = 18;
  maxAge = 99;
  pageNumber = 1;
  pageSize = 6;
  orderBy = 'lastActive';
  search = '';

  constructor(user: User) {
    this.gender = user.gender === 'female' ? 'male' : 'female'
  }
}
