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
  roomTypeId: number,
  name: string
}

export interface Building {
  buildingId: number,
  name: string,
}
