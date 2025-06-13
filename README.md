# BehaviourTree
1.	Create > ScriptableObjects > BehaviourTreeからアセットを作成
2.	ダブルクリックでノードエディタを開く
3.	ノードエディタ上で右クリックすると、配置するノードを選択可能
	Actionノードの処理内容はBehaviourTreeLib.ActionClassを継承したクラスを定義することでユーザー独自の処理を追加することができる。
4.	ノードエディタ左上のSaveで保存
	作成したアセットのデータが保存される。
5.	BehaviourTreeAIコンポーネントを付けたGameObjectを用意し、作成したアセットをアタッチする。
6.	実行
