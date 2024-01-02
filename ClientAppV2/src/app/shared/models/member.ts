import { Photo } from "./photo";

export interface Member {
  id: number;
  userName: string;
  firstName: string;
  lastName: string
  email: string;
  photoUrl: string;
  age: number;
  created: Date;
  lastActive: Date;
  gender: string;
  city: string;
  country: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  relationshipStatus: string;
  education: string;
  work: string;
  photos: Photo[];
}
