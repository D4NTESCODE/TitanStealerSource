<?php

namespace App\Exports;

use App\Models\Cookie;
use Maatwebsite\Excel\Concerns\FromCollection;

class CookiesExport implements FromCollection
{
    public function __construct(
        private int $count
    ) { }

    /**
    * @return \Illuminate\Support\Collection
    */
    public function collection()
    {
        $data = Cookie::all();
        
        if($this->count > 0) {
            return $data->take($this->count);
        }
        else {
            return $data;
        }
    }
}
