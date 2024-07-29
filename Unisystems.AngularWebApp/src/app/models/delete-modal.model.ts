export class DeleteModal {
  entityId: number;
  title: string;
  message: string;
  deleteAction: () => void;

  constructor() {
    this.entityId = 0;
    this.title = '';
    this.message = '';
    this.deleteAction = () => {};
  }
}
