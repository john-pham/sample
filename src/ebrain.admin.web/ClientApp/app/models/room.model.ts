// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

export class Room {
    // Note: Using only optional constructor properties without backing store disables typescript's type checking for the type
    constructor(roomId?: string) {
        this.id = roomId;
    }

    public id: string;
    public name: string;
    public code: string;
    public note: string;

}