export interface Notification {
  id: number;
  content: string;
  isRead: boolean;
  createdAt: Date;

  userId: number;
  username: string;
}
