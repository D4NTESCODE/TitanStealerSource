<?php

namespace App\Exports;

use App\Models\Autofill;
use Maatwebsite\Excel\Concerns\FromCollection;

class AutofillsExport implements FromCollection
{
    public function __construct(
        private int $count
    ) { }

    /**
    * @return \Illuminate\Support\Collection
    */
    public function collection()
    {
        $data = Autofill::all();
        
        if($this->count > 0) {
            return $data->take($this->count);
        }
        else {
            return $data;
        }
    }
}
