export interface User {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  photoUrl: string;
  city: string;
  country: string;
  gender: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  relationshipStatus: string;
  education: string;
  work: string;
  profileVisibility: boolean;
  token: string;
  roles: string[];
  thisUserFriendIds: number[];
  thisBlockedUsersIds: number[];
}
