export class CoordinatesModel {
    public latitude: number;
    public longitudue: number;

    constructor(long?: number, lat?: number) {
        this.longitudue = long;
        this.latitude = lat;
    }
}