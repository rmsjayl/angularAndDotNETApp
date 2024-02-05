export interface BlogPost {
  id: string;
  title: string;
  shortDescription: string;
  content: string;
  imageUrl: string;
  urlHandle: string;
  author: string;
  createdAt: Date;
  isVisible: boolean;
}
