export interface Photo {
  id: number;
  url: string;
  isMain: boolean;
  isApproved: boolean;
  averageVote: number;
  userVote: number;
  
  username?: string;
}
