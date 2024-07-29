export interface Classroom {
  classroomId: number,
  name: string,
  capacity: number,
  floor: number,
  number: number,
  roomType: RoomType,
  building: Building
}

export interface RoomType {
  roomTypeId: string,
  name: string
}

export interface Building {
  buildingId: number,
  name: string,
}
