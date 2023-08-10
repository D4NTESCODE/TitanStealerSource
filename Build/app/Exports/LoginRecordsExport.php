<?php

namespace App\Exports;

use App\Models\LoginRecord;
use Maatwebsite\Excel\Concerns\FromCollection;

class LoginRecordsExport implements FromCollection
{
    public function __construct(
        private int $count
    ) { }

    /**
    * @return \Illuminate\Support\Collection
    */
    public function collection()
    {
        $data = LoginRecord::all();
        
        if($this->count > 0) {
            return $data->take($this->count);
        }
        else {
            return $data;
        }
    }
}
