import { Photo } from './photo'

export interface Member {
  id: number
  userName: string
  photoUrl: string
  age: number
  aka: string
  created: Date
  lastActive: Date
  gender: string
  introduction: string
  lookingFor: string
  interests: string
  forest: string
  country: string
  photos: Photo[]
}
