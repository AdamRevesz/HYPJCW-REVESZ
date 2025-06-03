export interface UserShortViewDto{
  username: string;
  isProfessional: boolean;
}

export interface UserViewDto{
  username: string;
  email: string;
  isProfessional: boolean;
  filePath: string;
  isAdmin: boolean;
}

export interface CommentViewDto{
  id: string;
  body: string;
  username: string;
  likes?: number;
  createdAt: string;
}

export interface CommentCreateDto{
  body: string;
}

export interface ContentShortViewDto {
  id: string;
  title: string;
  body: string;
  filePath: string;
  approvalRate: string;
  ownerDetails: UserShortViewDto;
  category: string;
}

export interface ContentViewDto{
id: string;
CreatedAt: Date;
filePath: string;
title: string;
body: string;
price: number;
numberOfLikes: number;
approvalRate: string;
numberOfDislikes: number;
category: string;
ownerDetails: UserShortViewDto;
}

export interface CourseShortViewDto{
  id: string;
  title: string;
  body: string;
  approvalRate: string;
  owner: UserShortViewDto;
  category: string;
}

export interface PictureShortViewDto{
  id: string;
  title: string;
  approvalRate: string;
  owner: UserShortViewDto;
  category: string;
}

export interface VideoShortViewDto{
  id: string;
  title: string;
  approvalRate: string;
  owner: UserShortViewDto;
  category: string;
}

export interface SalesItemShortViewDto{
  id: string;
  filePath: string;
  body: string;
  title: string;
  price: number;
  approvalRate: string;
  type: string;
  inStock: boolean;
}

//create interfaces

export interface ContentCreateDto{
  title: string;
  body: string;
  tags: string;
  ownerId: string;
}

export interface PictureCreateDto{
title: string;
body: string;
category: string;
ownerId: string;
}

export interface SalesItemCreateDto{
  title: string;
  body: string;
  type: string;
  price: number;
  inStock: boolean;
  number: number;
}

export interface CourseCreateDto{
  title: string;
  filePath: string;
  body: string;
  category: string;
  price: number;
  courseCategory: Enumerator;
}

//User Dtos
export interface UserCreateUpdateDto {
  id: string;
  username: string;
  password: string;
  email: string;
  isProfessional: boolean;
}

//Graph helpers
export interface UserContentCountDto{
  username: string;
  contentCount: number;
}

export interface ContentCategoryCountDto {
  category: string;
  count: number;
}
