export interface UserShortViewDto{
  username: string;
  isProfessional: boolean;
}

export interface CommentViewDto{
  username: UserShortViewDto;
  id: string;
  body: string;
}

export interface ContentShortViewDto {
  id: string;
  width: 0;
  height: 0;
  title: string;
  body: string;
  filePath: string;
  approvalRate: string;
  owner: UserShortViewDto;
  category: string;
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
  title: string;
  approvalRate: string;
  owner: UserShortViewDto;
  category: string;
}

export interface ContentCreateDto{
  title: string;
  body: string;
  tags: string;
  ownerId: string;
}

export interface PictureCreateDto{
title: string;
filePath: string;
body: string;
category: string;
ownerId: string;
}
