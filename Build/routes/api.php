<?php

use App\Models\User;
use Filament\Notifications\Notification;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider and all of them will
| be assigned to the "api" middleware group. Make something great!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::prefix('callbacks')->group(function() {
    Route::post('/notify', function(Request $request) {
        $actions = [];

        foreach($request->get('actions') as $action) {
            $actions[] = \Filament\Notifications\Actions\Action::make($action['key'])->url($action['value']);
        }

        foreach (User::query()->get() as $user) {
            Notification::make()
                ->title('Titan Notification')
                ->body($request->get('message'))
                ->actions($actions)
                ->sendToDatabase($user);
        }

        return response()->json([
            'message' => 'Ok'
        ]);
    });

    Route::post('/build', function(\App\Http\Requests\OnBuildResultRequest $request) {
       \App\Service\Build\BuildService::on($request);
    });
});
