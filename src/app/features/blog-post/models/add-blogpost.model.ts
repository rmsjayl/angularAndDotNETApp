export interface AddBlogpost {
  title: string;
  shortDescription: string;
  content: string;
  imageUrl: string;
  urlHandle: string;
  author: string;
  createdAt: Date;
  isVisible: boolean;
}
